using FileReader;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Tests
{
    [TestClass]
    public class UnitTest
    {
        [TestMethod]
        public void FindDate()
        {
            string date = "Today's date is 19th of May, 2020.";
            TextFile text = new TextFile("filepath");
            TextFile.FindDate(date);
            Assert.AreEqual(new DateTime(2020, 5, 19), text.GetDate());               
        }
    }
}
