using System;
using System.IO;
using System.Text;

namespace LineCounter
{
    class Program
    {
        static void CreateConfig()
        {
            StringBuilder config = new StringBuilder();

            config.AppendLine("cs DEFAULT");
            config.AppendLine("h DEFAULT");
            config.AppendLine("c DEFAULT");
            config.AppendLine("hpp DEFAULT");
            config.AppendLine("cpp DEFAULT");
            config.AppendLine("fs // (* *)");
            config.AppendLine("vb '");
            config.AppendLine("txt");
            config.AppendLine("xml");
            config.AppendLine("fx DEFAULT");

            File.WriteAllText("config.txt", config.ToString());
        }

        static void Main(string[] args)
        {
            if (args.Length != 1 && (args.Length != 2 || args[1] != "/s"))
            {
                Console.WriteLine("Reports on line statistics for Visual Studio solutions.");
                Console.WriteLine();
                Console.WriteLine("Usage: LineCounter.exe filename [/s]");
            }
            else
            {
                bool silent = args.Length == 2;

                if (!File.Exists("config.txt"))
                    CreateConfig();

                string[] configuration = File.ReadAllLines("config.txt");

#if !DEBUG
                try
                {
#endif
                Counter counter = new Counter(args[0], configuration);
                counter.Calculate();

                if (!silent)
                {
                    foreach (string s in counter.Warnings)
                        Console.WriteLine("Warning: " + s);

                    Console.WriteLine("Projects    : " + counter.Projects);
                    Console.WriteLine("Files       : " + counter.Files);
                    Console.WriteLine("Directories : " + counter.Directories);
                    Console.WriteLine("Total Lines : " + counter.TotalLines);
                    Console.WriteLine("Code Lines  : " + counter.CodeLines);
                    Console.WriteLine("Comments    : " + counter.Comments);
                    Console.WriteLine("Blank Lines : " + counter.BlankLines);
                }
#if !DEBUG
                }
                catch (Exception e)
                {
                    if (!silent)
                        Console.WriteLine("Error: " + e.Message);
                }
#endif
            }

#if DEBUG
            Console.ReadLine();
#endif
        }
    }
}
