using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;


namespace ConsoleApp5
{
    class Program
    {

        private static string GetBearerToken()
        {
            string bearerToken = null;

            string authority = "https://login.microsoftonline.com/common/oauth2/"+ "78eff5bb-da38-47f0-a836-294c6d784112";
            string clientId = "bca42905-7439-47c7-a349-ef064fa6e8d6";
            string clientSecret = "AWq9PpVl-@r_TpHTIoigicsX4?s5yHa4";
            string resource = "https://management.core.windows.net/";

            Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext authContext = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(authority);
            Task<Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationResult> authResultTask = authContext.AcquireTokenAsync(resource, new Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential(clientId, clientSecret));

            bearerToken = authResultTask.Result.AccessToken;

            return bearerToken;

        }

        private static Task<HttpResponseMessage> HttpGet(string resourceUri)
        {
            HttpClient client = new HttpClient();
           string bearerToken = GetBearerToken();

            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
            //client.DefaultRequestHeaders.Add("x-ms-version", "2017-11-09");
            //client.DefaultRequestHeaders.Add("x-ms-blob-type", "BlockBlob");
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client.GetAsync(resourceUri);
        }


        static void Main(string[] args)
        {
           

            string subscriptionListUri = "https://management.azure.com/subscriptions?api-version=2019-11-01";
     
            Task<HttpResponseMessage> getSubscriptionListTask = HttpGet(subscriptionListUri);

            if (getSubscriptionListTask.Result.IsSuccessStatusCode)
            {
                var response = getSubscriptionListTask.Result.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<APIResponse>(response.ToString());
                var id = result.value.FirstOrDefault().id.ToString().Split('/')[2];

                var tt = "";
                
            }

            else if (getSubscriptionListTask.Result.IsSuccessStatusCode == false)
            {
                //throw new ApiException
                //{
                //    StatusCode = (int)response.StatusCode,
                //    Content = content
                //};
            }
            else
                {
                    //throw new Exception($"Create backup file failed with error - Status Code: {backupConfigFileTask.Result.StatusCode} Reason: {backupConfigFileTask.Result.ReasonPhrase}");
                }
            }
        }
    }




//string clientId = "f62903b9-ELIDED";
//string tenantId = "47b6e6c3-ELIDED";
//const string redirectUri = "urn:ietf:wg:oauth:2.0:oob";
//const string baseAuthUri = "https://login.microsoftonline.com/";
//const string resource = "https://management.core.windows.net/";

//var ctx = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext(baseAuthUri + tenantId);
////var ctx = new Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext("https://login.microsoftonline.com/common");
//var authResult = ctx.AcquireTokenAsync(resource, clientId, new Uri(redirectUri), PromptBehavior.Auto);
//var token = new TokenCredentials(authResult.AccessToken);
//var subClient = new SubscriptionClient(token);

//var tenants = await subClient.Tenants.ListAsync();
//foreach (var tenant in tenants) Console.WriteLine(tenant.TenantId);

//var subs = await subClient.Subscriptions.ListAsync();
//foreach (var sub in subs) Console.WriteLine(sub.DisplayName);

//string bearerToken = GetBearerToken();