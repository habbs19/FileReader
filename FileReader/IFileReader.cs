using System;
using System.Collections.Generic;
using System.Text;

namespace FileReader
{
    public interface IFileReader
    {
        public void ReadFile(Func<string,object> actionMethod);

        public IDictionary<string, string> GetLeetWords();

        public IDictionary<string, string> GetChapters();

        public IEnumerable<string> GetPhoneNumbers();

        public DateTime GetDate();



    }
}
