using Newtonsoft.Json;
using System.Net.Http;

namespace EV_HACIENDA.Servicios
{
    public class GenerarToken
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public GenerarToken(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }
            public async Task<string> ObtenerTokenAsync()
        {
            var client = _httpClientFactory.CreateClient();

            var tokenUrl = _configuration["Hacienda:TokenUrl"];
            var clientId = _configuration["Hacienda:ClientId"];
            var username = _configuration["Hacienda:Username"];
            var password = _configuration["Hacienda:Password"];

            var content = new FormUrlEncodedContent(new[]
            {
        new KeyValuePair<string, string>("client_id", clientId),
        new KeyValuePair<string, string>("username", username),
        new KeyValuePair<string, string>("password", password),
        new KeyValuePair<string, string>("grant_type", "password")
    });

            var response = await client.PostAsync(tokenUrl, content);
            var responseString = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Failed to retrieve token: {responseString}");
            }

            var tokenResponse = JsonConvert.DeserializeObject<dynamic>(responseString);
            return tokenResponse.access_token;
        }
    }
}
