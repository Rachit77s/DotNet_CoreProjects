using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Subscriptions;
using Microsoft.Azure;
using System.Threading;
using System.Net.Http;

namespace ConsoleApp8
{
    class Program
    {
        private const string ClientId = "bca42905-7439-47c7-a349-ef064fa6e8d6";
        private const string ServicePrincipalPassword = "AWq9PpVl-@r_TpHTIoigicsX4?s5yHa4";
        private const string AzureTenantId = "78eff5bb-da38-47f0-a836-294c6d784112";
        private const string AzureSubscriptionId = "4ad9dc7c-fc5f-4abd-8426-948fd3eae63c";

        private static string GetAuthorizationToken()
        {
            //ClientCredential uses Microsoft.IdentityModel.Clients.ActiveDirectory;
            ClientCredential cc = new ClientCredential(ClientId, ServicePrincipalPassword);
            var context = new AuthenticationContext("https://login.windows.net/" + AzureTenantId);
           
            var result = context.AcquireTokenAsync("https://management.core.windows.net/", cc);
            if (result == null)
            {
                throw new InvalidOperationException("Failed to obtain the JWT token");
            }

            return result.Result.AccessToken;
        }

        static void  Main(string[] args)
        {

            //foreach (var subscription in subscriptionClient.Subscriptions.ListAsync(cancelToken))
            //{
            //    Console.WriteLine(subscription.SubscriptionName);
            //}

            //TokenCredentials = new TokenCredentials(AuthToken);
            //CreateAzureResourceGroup(TokenCredentials);

            string resourceUri = "https://management.azure.com/subscriptions?api-version=2019-11-01";

            Console.WriteLine("Hello World!");

            string AuthToken = GetAuthorizationToken();

            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AuthToken);
            //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            //client.GetAsync(resourceUri);

            var tokenCred = new TokenCloudCredentials(AuthToken);

            //public SubscriptionClient(CloudCredentials credentials);          
            //public SubscriptionClient(HttpClient httpClient);         
            //public SubscriptionClient(CloudCredentials credentials, Uri baseUri);        
            //public SubscriptionClient(CloudCredentials credentials, HttpClient httpClient);        
            //public SubscriptionClient(CloudCredentials credentials, Uri baseUri, HttpClient httpClient);

            //new Uri("https://management.azure.com/subscriptions?api-version=2019-11-01")


            var subscriptionClient = new SubscriptionClient(tokenCred, new Uri("https://management.azure.com/subscriptions?api-version=2019-11-01"));




            var cancelToken = new CancellationToken();
            var tenantSubscriptions = subscriptionClient.Subscriptions.ListAsync(cancelToken).Result;

            //foreach (var subscription in subscriptionClient.Subscriptions.List())
            //{
            //    Console.WriteLine(subscription.SubscriptionName);
            //}

            foreach (var subscription in tenantSubscriptions.Subscriptions)
            {
                Console.WriteLine(subscription.SubscriptionName);
            }

        }
    }
}
