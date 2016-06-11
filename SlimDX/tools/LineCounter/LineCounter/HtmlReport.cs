using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using LineCounter.Properties;

namespace LineCounter
{
    class HtmlReport
    {
        string outputDirectory;

        public HtmlReport(string outputDirectory)
        {
            this.outputDirectory = outputDirectory;
        }

        public void Output(Node rootNode)
        {
            string directory = outputDirectory;
            Directory.CreateDirectory(directory);

            File.WriteAllText(Path.Combine(directory, "default.css"), Resources._default);

            directory = Path.Combine(directory, "img");
            Directory.CreateDirectory(directory);

            Resources.bg.Save(Path.Combine(directory, "bg.gif"));
            Resources.bgcontainer.Save(Path.Combine(directory, "bgcontainer.gif"));
            Resources.bgfooter.Save(Path.Combine(directory, "bgfooter.gif"));
            Resources.bgheader.Save(Path.Combine(directory, "bgheader.gif"));
            Resources.bgnavigation.Save(Path.Combine(directory, "bgnavigation.gif"));
            Resources.bgul.Save(Path.Combine(directory, "bgul.gif"));
            Resources.li.Save(Path.Combine(directory, "li.gif"));
            Resources.quote.Save(Path.Combine(directory, "quote.gif"));
            Resources.sidenavh1.Save(Path.Combine(directory, "sidenavh1.gif"));

            OutputHtml(rootNode);
        }

        void OutputHtml(Node rootNode)
        {
            FileNode results = Counter.Count(rootNode);

            string indexPage = Resources.index.Replace("$Title", rootNode.Name);
            StringBuilder content = new StringBuilder();
            content.AppendLine("<h1>Solution Overview</h1>");

            content.Append("<p>Project Count: ");
            content.Append(rootNode.ChildNodes.Count);
            content.AppendLine("<br>");

            content.Append("Folder Count: ");
            content.Append(Counter.CountFolders(rootNode, false));
            content.AppendLine("<br>");

            content.Append("Total Line Count: ");
            content.Append(results.TotalLines);
            content.AppendLine("<br>");
            content.AppendLine("<ul>");

            content.Append("<li>Code Lines: ");
            content.Append(results.CodeLines);
            content.AppendLine("</li>");

            content.Append("<li>Blank Lines: ");
            content.Append(results.BlankLines);
            content.AppendLine("</li>");

            content.Append("<li>Comments: ");
            content.Append(results.Comments);
            content.AppendLine("</li>");

            content.AppendLine("</ul></p>");
            indexPage = indexPage.Replace("$Content", content.ToString());

            File.WriteAllText(Path.Combine(outputDirectory, "index.html"), indexPage);
        }
    }
}
