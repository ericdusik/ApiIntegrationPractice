using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using LanguageDetectServiceTests.DTO;

namespace LanguageDetectServiceTests
{
    
    class LanguageTests
    {
        [Category("Feature.EnglishLanguage")]
        [Category("TestType.GoldFlow")]
        [Test, Description("Verify goldflow index creation using a REST call")]

        [TestCase("en", @"I like to eat hamburgers")]
        [TestCase("ru", @"Мне нравится есть корнеплоды")]
        [TestCase("ja", @"私は麺を食べたい")]
        [TestCase("el", @"Μου αρέσει να τρώω πίτα")]
        public void englishLangTest(string expected, string text)
        {
            var client = new RestClient("http://ws.detectlanguage.com");
            var request = new RestRequest("/0.2/detect", Method.POST);

            request.AddParameter("key", "demo"); // replace "demo" with your API key
            request.AddParameter("q", text);

            IRestResponse response = client.Execute(request);

            RestSharp.Deserializers.JsonDeserializer deserializer = new RestSharp.Deserializers.JsonDeserializer();

            var result = deserializer.Deserialize<Result>(response);

            Detection detection = result.data.detections[0];

            Console.WriteLine("Language: {0}", detection.language);
            Console.WriteLine("Reliable: {0}", detection.isReliable);
            Console.WriteLine("Confidence: {0}", detection.confidence);

            Assert.AreEqual(expected, detection.language, "we expected the language to be en but instead it was {0}", detection.language);
        }
    }
}