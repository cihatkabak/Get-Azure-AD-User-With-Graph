using Newtonsoft.Json;
using RestSharp;
using System;
using System.Configuration;

namespace GetADUsersWithGraph
{
    public class AzureUser
    {
        private RestClient client;
        private string token;
        public GraphUserList GetAzureUsers(string token)
        {
            client = new RestClient(ConfigurationManager.AppSettings["resource"]);
            var userRequest = new RestRequest("v1.0/Users?$top=999&$select=id", Method.GET);

            userRequest.AddHeader("Authorization", $"Bearer {token}");
            userRequest.AddHeader("Content-Type", "application/json");

            IRestResponse userResponse = client.Execute(userRequest);
            if (userResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<GraphUserList>(userResponse.Content);
            }
            else
            {
                return null;
            }
        }
        public GraphUserResponseList GetUserById(string token, GraphUserList graphUserList)
        {
            int counter = 0;
            this.token = token;
            GraphUserResponseList graphUsers = new GraphUserResponseList();
            GraphBatchRequestList requestArray = new GraphBatchRequestList();
            foreach (var user in graphUserList.Value)
            {
                string url = String.Format("users/{0}?$select=id,displayName,userPrincipalName,birthday,hireDate,givenName,surname,jobTitle,mobilePhone,officeLocation,businessPhones,mail,userType,department", user.Id);
                requestArray.Requests.Add(new GraphBatchRequest { Id = user.Id, Method = "GET", Url = url });
                counter++;
                if (counter % 20 == 0 || (graphUserList.Value.Count - graphUsers.Responses.Count < 20))
                {
                    client = new RestClient(ConfigurationManager.AppSettings["resource"]);
                    var userRequest = new RestRequest("v1.0/$batch", Method.POST);

                    userRequest.AddHeader("Authorization", $"Bearer {token}");
                    userRequest.AddHeader("Content-Type", "application/json");
                    userRequest.AddJsonBody(JsonConvert.SerializeObject(requestArray));

                    IRestResponse userResponse = client.Execute(userRequest);
                    if (userResponse.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        var users = JsonConvert.DeserializeObject<GraphUserResponseList>(userResponse.Content);
                        foreach (var item in users.Responses)
                        {
                            item.Body.Manager = GetManagerUserPrincipalName(item.Body.Id);
                            graphUsers.Responses.Add(item);
                        }
                    }
                    else
                    {
                        Console.WriteLine("GetUserById Method= " + userResponse.StatusCode);
                    }
                    requestArray.Requests.Clear();
                }
            }
            return graphUsers;
        }
        public GraphUser GetManagerUserPrincipalName(string userId)
        {
            GraphUser manager = null;
            GraphBatchRequestList requestArray = new GraphBatchRequestList();
            string url = String.Format("users/{0}?$expand=manager", userId);
            requestArray.Requests.Add(new GraphBatchRequest() { Id = userId, Method = "GET", Url = url });
            client = new RestClient(ConfigurationManager.AppSettings["resource"]);
            var userRequest = new RestRequest("beta/$batch", Method.POST);

            userRequest.AddHeader("Authorization", $"Bearer {token}");
            userRequest.AddHeader("Content-Type", "application/json");
            userRequest.AddJsonBody(JsonConvert.SerializeObject(requestArray));

            IRestResponse userResponse = client.Execute(userRequest);
            if (userResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var users = JsonConvert.DeserializeObject<GraphUserResponseList>(userResponse.Content);
                manager = users.Responses[0].Body.Manager;
                if (manager != null)
                {
                    return manager;
                }
            }
            return manager;
        }
       
    }
}
