using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EV_HACIENDA.Models;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using System.Security.Cryptography.Xml;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System.Xml.Linq;
using NuGet.Common;
using EV_HACIENDA.Servicios;


namespace EV_HACIENDA.Controllers
{
    public class FacturaElectronicaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<FacturaElectronicaController> _logger;
        private readonly GenerarXml _GenerarXml;
        private readonly GenerarToken _GenerarToken;
        private readonly EnviarDatos _EnviarDatos;
        private readonly GenerarFirmaXml _GenerarFirmaXml;
        private readonly PasearXML _PasearXML;

        public FacturaElectronicaController(
            ApplicationDbContext context,
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            GenerarXml GenerarXml,
            EnviarDatos EnviarDatos,
            GenerarFirmaXml GenerarFirmaXml,
            GenerarToken GenerarToken,
            PasearXML PasearXML,
        ILogger<FacturaElectronicaController> logger)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _GenerarXml = GenerarXml;
            _EnviarDatos = EnviarDatos;
            _GenerarFirmaXml = GenerarFirmaXml;
            _logger = logger;
            _GenerarToken = GenerarToken;
            _PasearXML = PasearXML;
        }



        public async Task<IActionResult> Index()
        {
            var facturas = await _context.FacturasElectronicas
               .Include(f => f.Emisor)
               .Include(f => f.Receptor)
               .Include(f => f.LineasDetalles)
               .Include(f => f.ResumenFactura)
               .ToListAsync();

            return View(facturas);
        }


        public async Task<IActionResult> Create()
        { 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacturaElectronica model)
        {


            var ultimoNumeroConsecutivo = await _context.FacturasElectronicas
                .OrderByDescending(f => f.FacturaElectronicaId) 
                .Select(f => f.NumeroConsecutivo) 
                .FirstOrDefaultAsync();
      
            var NuevoConsecutivo =  GenerarConsecutivo(ultimoNumeroConsecutivo);

            var factura = new FacturaElectronica
            {
                Clave = model.Clave,
                NumeroConsecutivo = NuevoConsecutivo,
                FechaEmision = DateTime.Now,
                ReceptorId = model.ReceptorId,
                ResumenFacturaId = 1,
                EstadoEnvio = "Sin Procesa",
                EmisorId = 1 
            };


            _context.FacturasElectronicas.Add(factura);
            await _context.SaveChangesAsync();
            var facturaId = factura.FacturaElectronicaId;

            foreach (var detalle in model.LineasDetalles)
            {
                if (detalle.Cantidad > 0)
                {
                    var lineaDetalle = new LineaDetalle
                    {
                        FacturaElectronicaId = facturaId,
                        Detalle = detalle.Detalle,
                        PrecioUnitario = detalle.PrecioUnitario,
                        MontoTotal = detalle.MontoTotal,
                        ImpuestoId =1,
                        CodigoId = 1,
                        Cantidad = detalle.Cantidad,
                        UnidadMedida = "Unidad",
                        MontoDescuento =  0.0m,
                        NaturalezaDescuento = ("0,0"),
                        SubTotal = detalle.SubTotal,
                        MontoTotalLinea = detalle.MontoTotalLinea
                    };

                    _context.LineasDetalles.Add(lineaDetalle);
                }
            }
   

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Upload()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile xmlFile)
        {
            if (xmlFile == null || xmlFile.Length == 0)
            {
                ViewBag.Message = "Por favor, seleccione un archivo XML.";
                return View();
            }

            try
            {
                using var stream = new MemoryStream();
                await xmlFile.CopyToAsync(stream);
                stream.Position = 0;
                var xmlContent = await new StreamReader(stream).ReadToEndAsync();
                var facturaMapeado = _PasearXML.ParseFacturaElectronica(xmlContent);
                using var transaction = await _context.Database.BeginTransactionAsync();
                Emisor emisor = facturaMapeado.Emisor;
                Receptor receptor = facturaMapeado.Receptor;
                ResumenFactura resumenFactura = facturaMapeado.ResumenFactura;

                
                 _context.Emisores.Add(emisor);
               
                 _context.Receptores.Add(receptor);
                
                 _context.ResumenFacturas.Add(resumenFactura);
                
                await _context.SaveChangesAsync();

                FacturaElectronica nuevaFactura = new FacturaElectronica
                {
                    EmisorId = emisor.EmisorId ,
                    ReceptorId = receptor.ReceptorId,
                    NumeroConsecutivo = facturaMapeado.NumeroConsecutivo,
                    Clave = facturaMapeado.Clave,
                    FechaEmision = facturaMapeado.FechaEmision,
                    ResumenFacturaId = resumenFactura.ResumenFacturaId ,
                    EstadoEnvio = facturaMapeado.EstadoEnvio,
                };

                _context.FacturasElectronicas.Add(nuevaFactura);
                await _context.SaveChangesAsync();

                var facturaId = nuevaFactura.FacturaElectronicaId;
                foreach (var lineaDetalle in facturaMapeado.LineasDetalles)
                {
                    lineaDetalle.FacturaElectronicaId = facturaId;
                    _context.LineasDetalles.Add(lineaDetalle);
                }
                await _context.SaveChangesAsync();

                // Confirmar la transacción
                await transaction.CommitAsync();

                ViewBag.Message = "Archivo XML subido y datos guardados correctamente.";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al subir el archivo XML. Detalles del error: {Message}", ex.Message);
                ViewBag.Message = "Error al subir el archivo XML.";
            }

            return View();
        }

        public async Task<IActionResult> Details(int id)
        {
            var factura = await _context.FacturasElectronicas
               .Include(f => f.Emisor)
               .Include(f => f.Receptor)
               .Include(f => f.ResumenFactura)
               .FirstOrDefaultAsync(f => f.FacturaElectronicaId == id);

            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EnviarFactura(int id)
        {

            var factura = await _context.FacturasElectronicas
              .Include(f => f.Emisor)
              .Include(f => f.Receptor)
              .Include(f => f.ResumenFactura)
              .FirstOrDefaultAsync(f => f.FacturaElectronicaId == id);
            if (factura == null)
            {
                return NotFound();
            }
        
            var xmlFactura = _GenerarXml.ConvertirFacturaAXml(factura);
            var xmlFirmado = _GenerarFirmaXml.FirmarXml(xmlFactura, _configuration["Certificate:Path"], _configuration["Certificate:Password"]);

            var token = await _GenerarToken.ObtenerTokenAsync();
            if (string.IsNullOrEmpty(token))
            {
                ViewBag.Error = "Error al obtener el token de autenticación.";
                return View("Details", factura);
            }

            var resultadoEnvio = await _EnviarDatos.EnviarFacturaAHaciendaAsync(factura,xmlFirmado, token);

            if (resultadoEnvio.ReasonPhrase.Equals("Accepted"))
            {
                factura.EstadoEnvio = "Aceptado";
            }
            else if  (resultadoEnvio.ReasonPhrase.Equals("Bad Request"))
            {
                factura.EstadoEnvio = "Rechazado";
            }
           
            _context.Update(factura);
            await _context.SaveChangesAsync();
            ViewBag.ResultadoEnvio = resultadoEnvio;
            return View("Details", factura);
        }

        public string GenerarConsecutivo(string ultimoConsecutivo)
        {
            const int longitudConsecutivo = 20; 

            if (string.IsNullOrEmpty(ultimoConsecutivo))
            {
                return "1".PadLeft(longitudConsecutivo, '0');
            }

            if (long.TryParse(ultimoConsecutivo, out long ultimoNumero))
            {
                long nuevoNumero = ultimoNumero + 1;
                return nuevoNumero.ToString().PadLeft(longitudConsecutivo, '0');
            }
            else
            {
                throw new ArgumentException("El último número consecutivo no es un número válido.");
            }
        }


    }

    public class TokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
    }
}
