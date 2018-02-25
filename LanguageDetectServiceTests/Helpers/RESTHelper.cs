using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using NUnit.Framework;
using LanguageDetectServiceTests.DTO;

namespace LanguageDetectServiceTests.Helpers
{

    //REST client adapted from: https://github.com/detectlanguage/detectlanguage-csharp/blob/master/example.cs

    public static class RESTHelper
    {

        public static IRestResponse Query(string clientEndPoint, string requestEndPoint, Dictionary<string, string> RESTQueryParameters)
        {
            RestClient client = new RestClient(clientEndPoint);
            RestRequest request = new RestRequest(requestEndPoint, Method.POST);

            //Let's add our parameters from our parameter dictionary
            foreach (KeyValuePair<string, string> pair in RESTQueryParameters)
            {
                request.AddParameter(pair.Key, pair.Value);
            }

            IRestResponse response = client.Execute(request);

            return response;
        }

    }
}
