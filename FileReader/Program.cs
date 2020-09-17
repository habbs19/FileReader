using System;
using System.Runtime.CompilerServices;
using System.IO;
using System.Collections.Generic;
using System.Net.Mail;

namespace FileReader
{
    class Program
    {
        static void Main(string[] args)
        {
            switch (args.Length)
            {
                case 0:
                    Console.WriteLine("File name required");
                    break;
                case 1:                    
                    string path = args[0];
                    if (File.Exists(path))
                    {
                        Console.WriteLine("Thank You. Beginning Process...");

                        IFileReader file = new TextFile(path);

                        DisplayAll(file);        
                    }
                    else
                    {
                        Console.WriteLine("File does not exist. Please enter filename found in current directory");
                    }
                    break;
                default:
                    Console.WriteLine("Invalid parameters");
                    break;
            }

            Console.WriteLine("\nProcess Complete");
        }

        public static void DisplayAll(IFileReader file)
        {
            var actionMethods = new List<Action<string>>{ TextFile.FindDate,TextFile.FindChapters,
                TextFile.FindLeet,TextFile.FindPhoneNumbers};
            file.ReadFile(actionMethods);

            WriteChapters(file);
            WriteLeet(file);
            WriteDate(file);
            WritePhoneNumbers(file);
        }

        public static void DisplayChapters(IFileReader file)
        {
            file.ReadFile(TextFile.FindChapters);
            WriteChapters(file);
        }

        public static void DisplayLeet(IFileReader file)
        {
            file.ReadFile(TextFile.FindLeet);
            WriteLeet(file);
        }
        public static void DisplayDate(IFileReader file)
        {
            file.ReadFile(TextFile.FindDate);
            WriteDate(file);
        }
        public static void DisplayPhoneNumbers(IFileReader file)
        {
            file.ReadFile(TextFile.FindPhoneNumbers);
            WritePhoneNumbers(file);
        }

        private static void WriteChapters(IFileReader file)
        {
            Console.WriteLine($"\nTotal chapters listed: {file.GetChapters().Count}");
        }
        private static void WriteLeet(IFileReader file)
        {
            Console.WriteLine($"\nLeet Translations: ");
            foreach (KeyValuePair<string, string> pair in file.GetLeetWords())
            {
                Console.WriteLine($"{pair.Key} {pair.Value}");
            }
        }
        private static void WriteDate(IFileReader file)
        {
            Console.WriteLine($"\nDate Found: {file.GetDate().ToString("yyyy-MM-dd")}");
        }

        private static void WritePhoneNumbers(IFileReader file)
        {
            Console.WriteLine("\nPhone Numbers Found:");
            foreach (string value in file.GetPhoneNumbers())
            {
                Console.WriteLine($"{value}");
            }
        }
    }
}
