using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using NUnit.Framework;
using LanguageDetectServiceTests.DTO;
using LanguageDetectServiceTests.Helpers;
using LanguageDetectServiceTests.Fixtures;

namespace LanguageDetectServiceTests.Tests.APITests
{
    
    class LanguageIDGoldFlow : TestFixture
    {

        [Category("Feature.PositiveLanguageID")]
        [Category("TestType.GoldFlow")]
        [Test, Description("Verify that we successfully verify languages as expected.")]

        [TestCase("en", @"I like to eat hamburgers")]
        //[TestCase("ru", @"Мне нравится есть корнеплоды")]
        //[TestCase("ja", @"私は麺を食べたい")]
        //[TestCase("el", @"Μου αρέσει να τρώω πίτα")]
        public void positiveLanguageIDTest(string expected, string text)
        {

            //ARRANGE
            Dictionary<string, string> RESTQueryParameters = new Dictionary<string, string>();
            RESTQueryParameters.Add("key", langAPIKey);
            RESTQueryParameters.Add("q", text);


            //ACT
            IRestResponse response = RESTHelper.Query(client, langRequest, RESTQueryParameters);

            RestSharp.Deserializers.JsonDeserializer deserializer = new RestSharp.Deserializers.JsonDeserializer();
            LangIDResult result = deserializer.Deserialize<LangIDResult>(response);

            Detection detection = result.data.detections[0];

            TestContext.Out.WriteLine("Language: {0}", detection.language);
            TestContext.Out.WriteLine("Reliable: {0}", detection.isReliable);
            TestContext.Out.WriteLine("Confidence: {0}", detection.confidence);


            //ASSERT
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual(expected, detection.language, "we expected the language to be en but instead it was {0}", detection.language);
        }
    }
}
