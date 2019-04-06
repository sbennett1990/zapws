using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zapws
{
    public class Program
    {
        private const string program = "zapws.exe";

        /*
         * TODO:
         *  - use command line args library
         *  - implement mode to delete white space from the file
         *  - implement switch to show full file vs. only the affected lines
         */
        public static void Main(string[] args)
        {
            if (args.Length < 1)
                Usage();

            var path = args[0];

            if (!File.Exists(path))
            {
                Console.WriteLine("invalid path or not a file");
                Quit();
            }

            var file = new FileInfo(path);

            Print(file);

            Console.WriteLine();
        }

        public static void Print(FileInfo file)
        {
            var stream = file.OpenRead();
            using (var fileReader = new StreamReader(stream))
            {
                int lineno = 0;
                string line;
                while ((line = fileReader.ReadLine()) != null)
                {
                    lineno++;
                    var reversed = line.Reverse().ToList();
                    if (!char.IsWhiteSpace(reversed.First()))
                    {
                        //Console.WriteLine(line);
                    }
                    else
                    {
                        for (int i = 0; i < reversed.Count; i++)
                        {
                            if (!char.IsWhiteSpace(reversed[i]))
                                break;

                            reversed[i] = '*';
                        }
                        reversed.Reverse();

                        var output = reversed.ToArray();
                        Console.Write("{0,4}  ", lineno);
                        Console.WriteLine(output);
                    }
                }

                fileReader.Close();
            }
        }

        public static void Quit()
        {
            Environment.Exit(1);
        }

        /// <summary>
        /// Print usage information to the console and terminate the app.
        /// </summary>
        public static void Usage()
        {
            Console.WriteLine("usage:");
            Console.WriteLine($"{program} filepath");
            Console.WriteLine();

            Environment.Exit(1);
        }
    }
}
