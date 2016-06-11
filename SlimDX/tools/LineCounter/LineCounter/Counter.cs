using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LineCounter
{
    class CommentProfile
    {
        public static CommentProfile Default = new CommentProfile()
        {
            LineComment = "//",
            StartComment = "/*",
            EndComment = "*/"
        };

        public static CommentProfile Empty = new CommentProfile();

        public string LineComment;
        public string StartComment;
        public string EndComment;
    }

    public class Counter
    {
        string inputFile;

        public List<string> Warnings = new List<string>();

        public int TotalLines
        {
            get;
            private set;
        }

        public int Comments
        {
            get;
            private set;
        }

        public int BlankLines
        {
            get;
            private set;
        }

        public int CodeLines
        {
            get;
            private set;
        }

        public int Directories
        {
            get;
            private set;
        }

        public int Files
        {
            get;
            private set;
        }

        public int Projects
        {
            get;
            private set;
        }

        List<string> fileTypes = new List<string>();
        Dictionary<string, CommentProfile> profiles = new Dictionary<string, CommentProfile>();

        public Counter(string inputFile, string[] configuration)
        {
            this.inputFile = inputFile;

            foreach (string c in configuration)
            {
                string[] parts = c.Trim().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 1 && parts.Length != 2 && parts.Length != 4)
                {
                    Warnings.Add("Invalid configuration line: " + c);
                    continue;
                }

                string extension = "." + parts[0].ToLower();
                fileTypes.Add(extension);

                if (parts.Length > 1)
                {
                    if (parts[1].ToLower() == "default")
                        profiles.Add(extension, CommentProfile.Default);
                    else
                    {
                        CommentProfile profile = new CommentProfile();
                        profile.LineComment = parts[1];

                        if (parts.Length > 2)
                        {
                            profile.StartComment = parts[2];
                            profile.EndComment = parts[3];
                        }

                        profiles.Add(extension, profile);
                    }
                }
                else
                    profiles.Add(extension, CommentProfile.Empty);
            }
        }

        public void Calculate()
        {
            Node rootNode = new Node();
            rootNode.Name = Path.GetFileNameWithoutExtension(inputFile);

            foreach (string line in File.ReadAllLines(inputFile).Where(s => s.StartsWith("Project", false, CultureInfo.InvariantCulture)))
            {
                string project = line.Extract('"', 5, 6);
                if (!project.Contains("proj"))
                    continue;

                project = Path.Combine(Path.GetDirectoryName(inputFile), project);

                if (project.Contains(".vcproj"))
                    rootNode.ChildNodes.Add(VCProject.Process(this, project));
                else
                {
                    Node node = CSProject.Process(this, project);
                    if( node != null )
                        rootNode.ChildNodes.Add(node);
                    else
                        Warnings.Add("Invalid project type: " + Path.GetExtension(project));
                }
            }

            if (rootNode.ChildNodes.Count == 0)
                throw new InvalidOperationException("No valid projects found.");

            FileNode results = Count(rootNode);

            CodeLines = results.CodeLines;
            Comments = results.Comments;
            BlankLines = results.BlankLines;
            TotalLines = results.TotalLines;

            Directories = CountFolders(rootNode, false);
            Projects = rootNode.ChildNodes.Count;
            Files = CountFiles(rootNode);
        }

        internal static int CountFolders(Node node, bool countTopLevel)
        {
            int count = 0;
            foreach (Node n in node.ChildNodes)
            {
                if (!(n is FileNode))
                {
                    if (countTopLevel)
                        count++;
                    count += CountFolders(n, true);
                }
            }

            return count;
        }

        internal static int CountFiles(Node node)
        {
            int count = 0;
            foreach (Node n in node.ChildNodes)
            {
                if (n is FileNode)
                    count++;

                count += CountFiles(n);
            }

            return count;
        }

        internal void CountFile(FileNode file)
        {
            string extension = Path.GetExtension(file.Path).ToLower();
            if (!fileTypes.Contains(extension))
            {
                file.Valid = false;
                return;
            }

            if (!File.Exists(file.Path))
                throw new InvalidOperationException("Could not open file " + file.Path);

            file.Valid = true;
            file.Name = Path.GetFileName(file.Path);

            string[] lines = File.ReadAllLines(file.Path);
            bool inComment = false;

            CommentProfile profile = CommentProfile.Empty;
            if (profiles.ContainsKey(extension))
                profile = profiles[extension];

            foreach (string line in lines)
            {
                file.TotalLines++;

                string temp = line.Trim();

                if (string.IsNullOrEmpty(temp))
                    file.BlankLines++;
                else if (inComment)
                {
                    file.Comments++;

                    if (temp.Contains(profile.EndComment))
                        inComment = false;
                }
                else
                {
                    if (profile.LineComment != null && temp.StartsWith(profile.LineComment))
                        file.Comments++;
                    else if (profile.StartComment != null && temp.Contains(profile.StartComment))
                    {
                        file.Comments++;
                        inComment = true;
                    }
                    else
                        file.CodeLines++;
                }
            }
        }

        internal static FileNode Count(Node node)
        {
            FileNode results = new FileNode();

            foreach (Node n in node.ChildNodes)
            {
                FileNode fn = n as FileNode;
                if (fn == null)
                    fn = Count(n);

                results.BlankLines += fn.BlankLines;
                results.CodeLines += fn.CodeLines;
                results.Comments += fn.Comments;
                results.TotalLines += fn.TotalLines;
            }

            return results;
        }
    }
}
