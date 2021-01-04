using System;
using System.IO;
using System.Linq;

namespace HeifConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            var converter = new Converter();
            if (args.Length == 0)
            {
                Console.WriteLine("add path and optional parameters (/o = overwrite, /s = include subfolders, /d = delete source)");
                return;
            }
            else
            {
                if (!Directory.Exists(args[0]))
                {
                    Console.WriteLine("invalid path. first argument must be a valid path");
                    return;
                }
            }
            bool includeSubFolders = args.Contains("/s");
            bool overwrite = args.Contains("/o");
            bool deleteSource = args.Contains("/d");
            converter.Start(args[0], includeSubFolders, overwrite, deleteSource);
        }
    }
}
