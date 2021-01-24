using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Visma.net_Payroll_Demo_Application
{
    class PayrollAPI
    {
        private async Task<string> GetToken()
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Post, "https://connect.visma.com/connect/token"))
                {
                    request.Content = new FormUrlEncodedContent(new Dictionary<string, string> {
                        { "grant_type", "client_credentials" },
                        { "client_id", "[YOUR_CLIENT_ID]" },
                        { "client_secret", "[YOUR_CLIENT_SECRET]" },
                        { "tenant_id", "[YOUR_TENANT_ID]" },
                        { "scope", "payroll:employees:read" }
                    });

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var payload = Newtonsoft.Json.Linq.JObject.Parse(await response.Content.ReadAsStringAsync());
                    var token = payload.Value<string>("access_token");

                    return token;
                }
            }
        }

        public async Task<int> GetNumberOfEmployees()
        {
            using (var client = new HttpClient())
            {
                using (var request = new HttpRequestMessage(HttpMethod.Get, "https://api.payroll.core.hrm.visma.net/v1/query/employees"))
                {
                    request.Headers.Add("Authorization", "Bearer " + await GetToken());
                    request.Headers.Add("Accept", "application/json");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    var payload = Newtonsoft.Json.Linq.JObject.Parse(await response.Content.ReadAsStringAsync());

                    Newtonsoft.Json.Linq.JArray employees = (Newtonsoft.Json.Linq.JArray)payload["data"];
                   
                    return employees.Count;
                }
            }
        }
    }
}
