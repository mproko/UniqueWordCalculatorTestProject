using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueWordsCalculator;
using static System.Net.Mime.MediaTypeNames;

namespace UniqueWordCalculatorTestProject
{
    [TestClass]
    public class FileUniqueWordReaderTests
    {
        private Dictionary<string, int> UniqueWordsList;
        private UniqueWordProcessing WordProcessing;
        private FileUniqueWordReader WordReader;
        private const int DEFAULT_BUFFER_SIZE = 1024;
        private Stream stream;
        private string TestWord = "Test1";

        [TestInitialize]
        public void Setup()
        {
            UniqueWordsList = new Dictionary<string, int>();
            WordProcessing = new UniqueWordProcessing(UniqueWordsList);
            WordReader = new FileUniqueWordReader(WordProcessing);
            WordReader.Buffer_Size = DEFAULT_BUFFER_SIZE;
        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        [TestMethod]
        public void Parse_One_New_Word_Empty_Dictionary()
        {
            TestWord = "Test1";
            stream = GenerateStreamFromString(TestWord);
            using (WordReader.FileManager = new StreamReader(stream))
                WordReader.ParseFileAsync();

            Assert.AreEqual(1, UniqueWordsList.Count());
            Assert.AreEqual(true, UniqueWordsList.ContainsKey(TestWord));
            Assert.AreEqual(1, UniqueWordsList[TestWord]);
        }

        [TestMethod]
        public void Parse_Two_Mixed_Words_Not_Empty_Dictionary()
        {
            WordProcessing.ProcessWord("Test1");
            stream = GenerateStreamFromString("  Test1   Test2   ");
            using (WordReader.FileManager = new StreamReader(stream))
                WordReader.ParseFileAsync();

            Assert.AreEqual(2, UniqueWordsList.Count());
            Assert.AreEqual(true, UniqueWordsList.ContainsKey("Test1"));
            Assert.AreEqual(2, UniqueWordsList["Test1"]);
            Assert.AreEqual(true, UniqueWordsList.ContainsKey("Test2"));
            Assert.AreEqual(1, UniqueWordsList["Test2"]);
        }



    }
}
