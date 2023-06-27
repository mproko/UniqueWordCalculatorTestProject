using UniqueWordsCalculator;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace UniqueWordCalculatorTestProject
{
    [TestClass]
    public class UniqueWordProcessingTests
    {
        private Dictionary<string, int> UniqueWordsList;
        private UniqueWordProcessing WordProcessing;
        private string TestWord = "Test1";

        [TestInitialize]
        public void Setup()
        {
            UniqueWordsList = new Dictionary<string, int>();
            WordProcessing = new UniqueWordProcessing(UniqueWordsList);
        }

        [TestMethod]
        public void ProcessWord_Add_New_Word_Empty_Dictionary()
        {
            TestWord = "Test1";
            WordProcessing.ProcessWord(TestWord);

            Assert.AreEqual(1, UniqueWordsList.Count());
            Assert.AreEqual(true, UniqueWordsList.ContainsKey(TestWord));
            Assert.AreEqual(1, UniqueWordsList[TestWord]);
        }

        [TestMethod]
        public void ProcessWord_Add_New_Word_Not_Empty_Dictionary()
        {
            WordProcessing.ProcessWord("Test1");
            TestWord = "Test2";
            WordProcessing.ProcessWord(TestWord);

            Assert.AreEqual(2, UniqueWordsList.Count());
            Assert.AreEqual(true, UniqueWordsList.ContainsKey(TestWord));
            Assert.AreEqual(1, UniqueWordsList[TestWord]);
        }

        [TestMethod]
        public void ProcessWord_Add_Existing_Word_Not_Empty_Dictionary()
        {
            WordProcessing.ProcessWord("Test1");
            WordProcessing.ProcessWord("Test2");
            WordProcessing.ProcessWord("Test3");
            var TestWord = "Test2";

            WordProcessing.ProcessWord(TestWord);

            Assert.AreEqual(3, UniqueWordsList.Count());
            Assert.AreEqual(true, UniqueWordsList.ContainsKey(TestWord));
            Assert.AreEqual(2, UniqueWordsList[TestWord]);
        }
    }
}
