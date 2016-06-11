using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ReleaseNotesGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
                Console.WriteLine("Usage: ReleaseNotesGenerator ReleaseNotes.txt");

            if (!File.Exists(args[0]))
                Console.WriteLine("Could not find target file: {0}", args[0]);

            var output = new StringBuilder();
            output.AppendLine("<list class=\"bullet\">");
            bool open = false;

            foreach (var l in File.ReadLines(args[0]))
            {
                string line = l.Trim();
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                if (!line.StartsWith("*"))
                {
                    if (open)
                    {
                        output.AppendLine("\t\t</list>");
                        output.AppendLine("\t</listItem>");
                    }

                    output.AppendLine("\t<listItem>");
                    output.AppendFormat("\t\t<legacyBold>{0}</legacyBold>", line).AppendLine();
                    output.AppendLine("\t\t<list class=\"bullet\">");

                    open = true;
                }
                else
                {
                    output.AppendFormat("\t\t\t<listItem>{0}</listItem>", line.TrimStart('*', ' ')).AppendLine();
                }
            }

            if (open)
            {
                output.AppendLine("\t\t</list>");
                output.AppendLine("\t</listItem>");
            }

            output.AppendLine("</list>");

            File.WriteAllText(Path.ChangeExtension(args[0], ".xml"), output.ToString());
        }
    }
}
