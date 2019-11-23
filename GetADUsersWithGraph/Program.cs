using System;
using System.Configuration;

namespace GetADUsersWithGraph
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RestHelper rest = new RestHelper();

                string resource = ConfigurationManager.AppSettings["resource"];
                string clientId = ConfigurationManager.AppSettings["graph_client_id"];
                string clientSecret = ConfigurationManager.AppSettings["graph_client_secret"];
                string tenant = new Uri(ConfigurationManager.AppSettings["TenantUrl"]).Host.Split('.')[0];

                ADALToken token = rest.AcquireTokenWithClientCredentials(tenant, resource, clientId, clientSecret);
                AzureUser azureUser = new AzureUser();
                if (!string.IsNullOrEmpty(token.AccessToken))
                {
                    GraphUserList azureUsersId = azureUser.GetAzureUsers(token.AccessToken);
                    if (azureUsersId != null)
                    {
                        GraphUserResponseList azureUsers = azureUser.GetUserById(token.AccessToken, azureUsersId);
                        Console.WriteLine(string.Format("{0} azure users has found...", azureUsers.Responses.Count));
                    }
                    else
                    {
                        Console.WriteLine("Azure user not found!");
                    }
                }
                else
                {
                    Console.WriteLine("Token is null!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }
}
