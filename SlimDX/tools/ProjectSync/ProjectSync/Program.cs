using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProjectSync
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 3)
                Console.WriteLine("Usage: ProjectSync vs2008project vs2010project prepend_ext");
            else
            {
                if (!File.Exists(args[0]))
                    Console.WriteLine("Could not open VS2008 project file.");
                else if (!File.Exists(args[1]))
                    Console.WriteLine("Could not open VS2010 project file.");
                else if (!File.Exists(args[1] + ".filters"))
                    Console.WriteLine("Could not open VS2010 filters file.");
                else
                    Synchronize(args[0], args[1], args[2]);
            }
        }

        static void Synchronize(string vs2008project, string vs2010project, string prepend)
        {
            var parser = new ProjectParser(vs2008project, prepend);
            var writer = new ProjectWriter(vs2010project);

            var filterWriter = new ProjectWriter(vs2010project + ".filters");
            filterWriter.GetFilterIdentifiers(parser.Filters);

            writer.UpdateItems(ItemType.Includes, parser.IncludeFiles, false);
            writer.UpdateItems(ItemType.Sources, parser.SourceFiles, false);
            writer.UpdateItems(ItemType.Resources, parser.ResourceFiles, false);
            writer.UpdateItems(ItemType.Other, parser.OtherFiles, false);

            filterWriter.UpdateItems(ItemType.Filters, parser.Filters, true);
            filterWriter.UpdateItems(ItemType.Includes, parser.IncludeFiles, true);
            filterWriter.UpdateItems(ItemType.Sources, parser.SourceFiles, true);
            filterWriter.UpdateItems(ItemType.Resources, parser.ResourceFiles, true);
            filterWriter.UpdateItems(ItemType.Other, parser.OtherFiles, true);

            writer.Save();
            filterWriter.Save();
        }
    }
}
