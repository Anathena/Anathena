using System.IO;
using System.Xml.Linq;

namespace LineCounter
{
    static class VCProject
    {
        static string projectPath;

        public static Node Process(Counter counter, string fileName)
        {
            projectPath = fileName;

            Node rootNode = new Node();
            rootNode.Name = Path.GetFileNameWithoutExtension(fileName);

            XDocument project = XDocument.Load(fileName);
            XElement rootFile = project.Element("VisualStudioProject").Element("Files");
            ProcessElement(counter, rootFile, rootNode);

            return rootNode;
        }

        static void ProcessElement(Counter counter, XElement element, Node rootNode)
        {
            foreach (XElement e in element.Elements("File"))
            {
                XAttribute attribute = e.Attribute("RelativePath");
                if (attribute == null)
                    continue;

                FileNode file = new FileNode();
                file.Path = Path.Combine(Path.GetDirectoryName(projectPath), attribute.Value);
                counter.CountFile(file);

                if (file.Valid)
                    rootNode.ChildNodes.Add(file);
            }

            foreach (XElement e in element.Elements("Filter"))
            {
                XAttribute attribute = e.Attribute("Name");
                if (attribute == null)
                    continue;

                Node node = new Node();
                node.Name = attribute.Value;
                ProcessElement(counter, e, node);

                rootNode.ChildNodes.Add(node);
            }
        }
    }
}
