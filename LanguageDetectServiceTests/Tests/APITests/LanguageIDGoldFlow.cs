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
using NUnit.Framework.Interfaces;

namespace LanguageDetectServiceTests.Tests.APITests
{
    
    class LanguageIDGoldFlow : TestFixture
    {

        [TearDown]
        public void logFailures()
        {
            testClassForReport = TestContext.CurrentContext.Test.ClassName.ToString();

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                Utility.logError(TestContext.CurrentContext, failedTestCases);
            }
            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            {
                passedTestCases = passedTestCases + 1;
            }
        }

        [Category("Feature.PositiveLanguageID")]
        [Category("TestType.GoldFlow")]
        [Test, Description("Verify that we successfully verify languages as expected.")]

        [TestCase("en", @"I like to eat hamburgers")]
        [TestCase("ru", @"Мне нравится есть корнеплоды")]
        [TestCase("ja", @"私は麺を食べたい")]
        [TestCase("el", @"Μου αρέσει να τρώω πίτα")]
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

        [Category("Feature.Confidence")]
        [Category("TestType.GoldFlow")]
        [Test, Description("Verify that sufficiently long text has a reliability score greater than 5.")]

        [TestCase("en", @"There, peeping among the cloud-wrack above a dark tor high up in the mountains, 
                          Sam saw a white star twinkle for a while. The beauty of it smote his heart, as he 
                          looked up out of the forsaken land, and hope returned to him. For like a shaft, 
                          clear and cold, the thought pierced him that in the end the Shadow was only a small 
                          and passing thing: there was a light and high beauty for ever beyond its reach.")]
        [TestCase("ru", @"Я уже давно так живу - лет двадцать. Теперь мне сорок. Я прежде служил, а теперь 
                          не служу. Я был злой чиновник. Я был груб и находил в этом удовольствие. Ведь я 
                          взяток не брал, стало быть, должен же был себя хоть этим вознаградить. (Плохая 
                          острота; но я ее не вычеркну. Я ее написал, думая, что выйдет очень остро; а теперь, 
                          как увидел сам, что хотел только гнусно пофорсить, - нарочно не вычеркну!)")]
        //[TestCase("ja", @"私は麺を食べたい")]
        //[TestCase("el", @"Μου αρέσει να τρώω πίτα")]
        public void languageReliabilityTest(string expected, string text)
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
            Assert.Greater(detection.confidence, 5.0);

        }

    }
}
