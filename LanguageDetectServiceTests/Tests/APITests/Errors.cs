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

    class Errors : TestFixture
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


        [Category("Feature.Authentication")]
        [Category("TestType.Error")]
        [Test, Description("Verify that an incorrect key returns an authorization error.")]

        [TestCase(1, "Invalid API key")]
        public void invalidAPIKey(int code, string message)
        {
            //ARRANGE
            Dictionary<string, string> RESTQueryParameters = new Dictionary<string, string>();
            RESTQueryParameters.Add("key", "not-a-real-key");


            //ACT
            IRestResponse response = RESTHelper.Query(client, langRequest, RESTQueryParameters);
            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            ErrorResponse result = deserial.Deserialize<ErrorResponse>(response);
            Error error = result.error;



            //ASSERT
            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
            Assert.AreEqual(1, error.code, "we expected the code to be 1 but instead it was {0}", error.code);

			
        }
     }
}



