using System;
using System.Collections.Generic;
using NUnit.Framework;
using RestSharp;
using LanguageDetectServiceTests.Helpers;
using LanguageDetectServiceTests.DTO;
using System.Diagnostics;
using System.IO;
using System.Web.Configuration;

namespace LanguageDetectServiceTests.Fixtures
{
    public class TestFixture
    {
        public readonly string client = "http://ws.detectlanguage.com";
        public readonly string langRequest = "/0.2/detect";
        public readonly string langAPIKey = WebConfigurationManager.AppSettings["apiKey"];

        private readonly string langAPIUsageRequest = "/0.2/user/status";
        

        [OneTimeTearDown]
        public void ReportOnAPIUsage()
        {


			Dictionary<string, string> usageQueryParameters = new Dictionary<string, string>();
            usageQueryParameters.Add("key", langAPIKey);

            //ACT
            IRestResponse response = RESTHelper.Query(client, langAPIUsageRequest, usageQueryParameters);

            RestSharp.Deserializers.JsonDeserializer deserial = new RestSharp.Deserializers.JsonDeserializer();
            Usage usage = deserial.Deserialize<Usage>(response);

            //Write our test report
            ReportWriter.writeReport(usage);


           


        }
    }
}
