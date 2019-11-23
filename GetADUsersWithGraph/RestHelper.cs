using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;

namespace GetADUsersWithGraph
{
    public class RestHelper
    {
        private RestClient client;
     
        public ADALToken AcquireTokenWithClientCredentials(string tenant, string resource, string clientId, string clientSecret)
        {

            ADALToken token = null;
            client = new RestClient("https://login.microsoftonline.com");

            var request = new RestRequest($"{tenant}.onmicrosoft.com/oauth2/token", Method.POST);

            request.AddParameter("client_id", clientId);
            request.AddParameter("grant_type", ConfigurationManager.AppSettings["grant_type"]);
            request.AddParameter("resource", resource);
            request.AddParameter("client_secret", clientSecret);

            IRestResponse response = client.Execute(request);
            var content = response.Content;
            if (!string.IsNullOrEmpty(content) && response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                try
                {
                    token = JsonConvert.DeserializeObject<ADALToken>(content);
                }
                catch { }
            }
            return token;
        }

    }
}