using Newtonsoft.Json.Linq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace VisualTokenizacionWConfig.Logic
{
    public class BibliotecaVariablesApi
    {
        private string url;
        HttpClient client;
        public BibliotecaVariablesApi(string url)
        {
            this.url = url;
            var uri = new Uri(url);
            var credentialsCache = new CredentialCache { { uri, "NTLM", CredentialCache.DefaultNetworkCredentials } };
            var handler = new HttpClientHandler { Credentials = credentialsCache };
            client = new HttpClient(handler) { BaseAddress = uri };
        }

        public async Task<JObject> ObtenerJSONDeVariables()
        {
            HttpResponseMessage response = await client.GetAsync(url);
            return JObject.Parse(response.Content.ReadAsStringAsync().Result);
        }
    }
}
