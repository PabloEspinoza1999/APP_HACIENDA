using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using EV_HACIENDA.Models;

namespace EV_HACIENDA.Servicios
{
    public class EnviarDatos
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        public EnviarDatos(IHttpClientFactory httpClientFactory , IConfiguration configuration)
        {
            _httpClientFactory=httpClientFactory;
            _configuration=configuration;

        }

        public async Task<HttpResponseMessage> EnviarFacturaAHaciendaAsync(FacturaElectronica factura, string xmlFirmado, string token)
        {
            var client = _httpClientFactory.CreateClient();

            var url = _configuration["Hacienda:RecepcionUrl"];
            var comprobanteXmlBase64 = Convert.ToBase64String(Encoding.UTF8.GetBytes(xmlFirmado));

            var jsonContent = new
            {
                clave = factura.Clave, 
                fecha = DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ssZ"),
                emisor = new { tipoIdentificacion = factura.Emisor.Identificacion, numeroIdentificacion = factura.Emisor.EmisorId },
                receptor = new { tipoIdentificacion = factura.Receptor.Identificacion, numeroIdentificacion = factura.ResumenFacturaId },
                comprobanteXml = comprobanteXmlBase64
            };

            var content = new StringContent(JsonConvert.SerializeObject(jsonContent), Encoding.UTF8, "application/json");

            // Añadiendo encabezado de autorización
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.PostAsync(url, content);

            return response;
        }
    }
}
