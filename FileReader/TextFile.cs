using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Linq;

namespace FileReader
{
    public class TextFile : IFileReader
    {
        private readonly string _filepath;
        private static Dictionary<string, string> _leetWords = new Dictionary<string, string>();
        private static Dictionary<string, string> _chapters = new Dictionary<string, string>();
        private static List<string> _phoneNumbers = new List<string>();
        private static DateTime _date;

        public TextFile(string filepath)
        {
            _filepath = filepath;
        }

        public IDictionary<string, string> GetLeetWords() => _leetWords;
        public IDictionary<string, string> GetChapters() => _chapters;
        public IEnumerable<string> GetPhoneNumbers() => _phoneNumbers;
        public DateTime GetDate() => _date == null ? new DateTime() : _date;

        public static void FindChapters(string line)
        {
            string pattern = @"^Chapter\s\d+\..+";
            if(Regex.IsMatch(line, pattern))
            {
                string[] chapterHeader = Regex.Split(line, @"^(Chapter\s\d+\.)");
                string chapter = chapterHeader[1];
                string chapterTitle = chapterHeader[2];

                if (!_chapters.ContainsKey(chapter))
                {
                    _chapters.Add(chapter, chapterTitle.Trim());
                }
            }
        }

        public static void FindLeet(string line)
        {
            string pattern1 = @"^[^aAeEgGiIoOsStT()]+$";
            string pattern2 = @"^(([\d]+)|([\d]+.)|(x[\d]+)|([\d]+-[\d]+)|([\d]+-[\d]+;)|([\d]+,[\d]+))$";
            string pattern3 = "^[&a-zA-Z,!.]+$";

            foreach (string word in line.Split(" ")){
                string parsedWord = word.Replace(",", "").Replace("?", "").Replace("\"", "").Replace("!", "").Replace(".", "").Replace("—", "").Replace("“", "").Replace("”","");
                if (Regex.IsMatch(parsedWord, pattern1) && !Regex.IsMatch(parsedWord, pattern2) && !Regex.IsMatch(parsedWord, pattern3))
                {
                    string englishWord = parsedWord.Replace('4', 'a').Replace('3', 'e').Replace('6', 'g').Replace('1', 'i').Replace('0', 'o').Replace('5', 's').Replace('7', 't');

                    if (!_leetWords.ContainsKey(parsedWord))
                    {
                        _leetWords.Add(parsedWord, englishWord);
                    }
                }
            }
        }

        public static void FindPhoneNumbers(string line)
        {
            string pattern = @"(\([\d]{3}\)\s[\d]{3}-[\d]{4}\sx[\d]+)|(\(\d{3}\)\s\d{3}-\d{4})";
            if (Regex.IsMatch(line, pattern))
            {
                foreach(Match m in Regex.Matches(line,pattern).AsEnumerable())
                {
                    _phoneNumbers.Add(m.Value);
                }
            }           
        }
        public static void FindDate(string line)
        {
            string pattern = @"\d+th\s\w+\s\w+,\s\d{4}";
            if (Regex.IsMatch(line, pattern))
            {
                int day = 1;
                int month = 1;
                int year = 1;
                var match = Regex.Match(line, pattern);
                string[] split = match.Value.Split(' ');               
                foreach(string s in split)
                {
                    if (Regex.IsMatch(s, @"^\d{4}$"))
                    {
                        year = Convert.ToInt32(s);
                        continue;
                    }

                    if (Regex.IsMatch(s, @"^\d{1,2}"))
                    {
                        day = Convert.ToInt32(Regex.Match(s, @"^\d{1,2}").Value);
                        continue;
                    }                   

                    if(Regex.IsMatch(s, @"^[a-zA-Z]+"))
                    {
                        string mth = Regex.Match(s, @"^[a-zA-Z]+").Value;

                        if (Enum.IsDefined(typeof(Months), mth))
                        {
                            month = Convert.ToInt32(Enum.Parse(typeof(Months), mth));
                        }
                    }
                }
                _date = new DateTime(year, month, day);
            }
        }

        public void ReadFile(Action<string> actionMethod)
        {
            foreach (string line in File.ReadLines(_filepath))
            {
                actionMethod(line);
            }
        }

        public enum Months
        {
            January=1,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        }

    }
}
