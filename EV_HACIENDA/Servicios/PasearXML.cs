using EV_HACIENDA.Models;
using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace EV_HACIENDA.Servicios
{
    public class PasearXML
    {
        public FacturaElectronica ParseFacturaElectronica(string xml)
        {
            var document = XDocument.Parse(xml);
            var ns = document.Root.GetDefaultNamespace();

            var factura = new FacturaElectronica
            {
                Clave = document.Descendants(ns + "Clave").First().Value,
                NumeroConsecutivo = document.Descendants(ns + "NumeroConsecutivo").First().Value,
                FechaEmision = DateTime.Parse(document.Descendants(ns + "FechaEmision").First().Value),
                Emisor = new Emisor
                {
                    Nombre = document.Descendants(ns + "Emisor").Descendants(ns + "Nombre").First().Value,
                    Identificacion = document.Descendants(ns + "Emisor").Descendants(ns + "Identificacion").Descendants(ns + "Numero").First().Value,
                    CorreoElectronico = document.Descendants(ns + "Emisor").Descendants(ns + "CorreoElectronico").First().Value,
                    Provincia = document.Descendants(ns + "Emisor").Descendants(ns + "Ubicacion").Descendants(ns + "Provincia").First().Value,
                    Canton = document.Descendants(ns + "Emisor").Descendants(ns + "Ubicacion").Descendants(ns + "Canton").First().Value,
                    Distrito = document.Descendants(ns + "Emisor").Descendants(ns + "Ubicacion").Descendants(ns + "Distrito").First().Value,
                    Barrio = document.Descendants(ns + "Emisor").Descendants(ns + "Ubicacion").Descendants(ns + "Barrio").First().Value,
                    OtrasSenas = document.Descendants(ns + "Emisor").Descendants(ns + "Ubicacion").Descendants(ns + "OtrasSenas").First().Value,
                    Telefono = document.Descendants(ns + "Emisor").Descendants(ns + "Telefono").Descendants(ns + "NumTelefono").First().Value,
                    CodigoPais = document.Descendants(ns + "Emisor").Descendants(ns + "Telefono").Descendants(ns + "CodigoPais").First().Value,
                },
                Receptor = new Receptor
                {
                    Nombre = document.Descendants(ns + "Receptor").Descendants(ns + "Nombre").First().Value,
                    Identificacion = document.Descendants(ns + "Receptor").Descendants(ns + "Identificacion").Descendants(ns + "Numero").First().Value,
                    CorreoElectronico = document.Descendants(ns + "Receptor").Descendants(ns + "CorreoElectronico").First().Value,
                },
                ResumenFactura = new ResumenFactura
                {
                    TotalGravado = decimal.Parse(document.Descendants(ns + "ResumenFactura").Descendants(ns + "TotalGravado").First().Value, CultureInfo.InvariantCulture),
                    TotalImpuesto = decimal.Parse(document.Descendants(ns + "ResumenFactura").Descendants(ns + "TotalImpuesto").First().Value, CultureInfo.InvariantCulture),
                    TotalComprobante = decimal.Parse(document.Descendants(ns + "ResumenFactura").Descendants(ns + "TotalComprobante").First().Value, CultureInfo.InvariantCulture)
                },
                EstadoEnvio = "Pendiente",

                LineasDetalles = document.Descendants(ns + "LineaDetalle").Select(ld => new LineaDetalle
                {
                    Detalle = ld.Element(ns + "Detalle").Value,
                    PrecioUnitario = decimal.Parse(ld.Element(ns + "PrecioUnitario").Value),
                    MontoTotal = decimal.Parse(ld.Element(ns + "MontoTotal").Value),
                    MontoDescuento = decimal.Parse(ld.Element(ns + "MontoDescuento").Value),
                    NaturalezaDescuento = ld.Element(ns + "NaturalezaDescuento").Value,
                    SubTotal = decimal.Parse(ld.Element(ns + "SubTotal").Value),
                    MontoTotalLinea = decimal.Parse(ld.Element(ns + "MontoTotalLinea").Value),
                    Cantidad = decimal.Parse(ld.Element(ns + "Cantidad").Value),
                    UnidadMedida = ld.Element(ns + "UnidadMedida").Value,
                    Codigo = new Codigo
                    {
                        Tipo = ld.Element(ns + "Codigo").Element(ns + "Tipo").Value,
                        CodigoValor = ld.Element(ns + "Codigo").Element(ns + "Codigo").Value
                    },
                    Impuesto = new Impuesto
                    {
                        Tarifa = decimal.Parse(ld.Element(ns + "Impuesto").Element(ns + "Tarifa").Value),
                        Monto = decimal.Parse(ld.Element(ns + "Impuesto").Element(ns + "Monto").Value)
                    }
                }).ToList()
            };

            return factura;
        }
    }
}
