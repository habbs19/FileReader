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

            Console.WriteLine($"\nTotal chapters listed: {file.GetChapters().Count}");
            Console.WriteLine($"\nLeet Translations: ");
            foreach (KeyValuePair<string, string> pair in file.GetLeetWords())
            {
                Console.WriteLine($"{pair.Key} {pair.Value}");
            }
            Console.WriteLine($"\nDate Found: {file.GetDate().ToString("yyyy-MM-dd")}");
            Console.WriteLine("\nPhone Numbers Found:");
            foreach (string value in file.GetPhoneNumbers())
            {
                Console.WriteLine($"{value}");
            }
        }

        public static void DisplayChapters(IFileReader file)
        {
            file.ReadFile(TextFile.FindChapters);
            Console.WriteLine($"\nTotal chapters listed: {file.GetChapters().Count}");
        }

        public static void DisplayLeet(IFileReader file)
        {
            file.ReadFile(TextFile.FindLeet);
            Console.WriteLine($"\nLeet Translations: ");
            foreach (KeyValuePair<string, string> pair in file.GetLeetWords())
            {
                Console.WriteLine($"{pair.Key} {pair.Value}");
            }
        }
        public static void DisplayDate(IFileReader file)
        {
            file.ReadFile(TextFile.FindDate);
            Console.WriteLine($"\nDate Found: {file.GetDate().ToString("yyyy-MM-dd")}");
        }
        public static void DisplayPhoneNumbers(IFileReader file)
        {
            file.ReadFile(TextFile.FindPhoneNumbers);
            Console.WriteLine("\nPhone Numbers Found:");
            foreach (string value in file.GetPhoneNumbers())
            {
                Console.WriteLine($"{value}");
            }
        }
    }
}
