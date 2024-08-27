using EV_HACIENDA.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EV_HACIENDA.Servicios
{
    public class GenerarXml
    {
        public string ConvertirFacturaAXml(FacturaElectronica factura)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement("TiqueteElectronico",
                    new XAttribute(XNamespace.Xmlns + "ds", "http://www.w3.org/2000/09/xmldsig#"),
                    new XElement("Clave", factura.Clave),
                    new XElement("CodigoActividad", "0H2"),
                    new XElement("NumeroConsecutivo", factura.NumeroConsecutivo),
                    new XElement("FechaEmision", factura.FechaEmision.ToString("yyyy-MM-ddTHH:mm:ss")),
                    new XElement("Emisor",
                        new XElement("Nombre", factura.Emisor.Nombre),
                        new XElement("Identificacion",
                            new XElement("Tipo", "01"),
                            new XElement("Numero", factura.Emisor.Identificacion)
                        ),
                        new XElement("Ubicacion",
                            new XElement("Provincia", factura.Emisor.Provincia),
                            new XElement("Canton", factura.Emisor.Canton), 
                            new XElement("Distrito", factura.Emisor.Distrito),
                            new XElement("Barrio", factura.Emisor.Barrio), 
                            new XElement("OtrasSenas", factura.Emisor.OtrasSenas) 
                        ),
                        new XElement("Telefono",
                            new XElement("CodigoPais", factura.Emisor.CodigoPais),
                            new XElement("NumTelefono", factura.Emisor.Telefono)
                        ),
                        new XElement("CorreoElectronico", factura.Emisor.CorreoElectronico)
                    ),
                    new XElement("Receptor",
                        new XElement("Nombre", factura.Receptor.Nombre),
                        new XElement("Identificacion",
                            new XElement("Tipo", "01"),
                            new XElement("Numero", factura.Receptor.Identificacion)
                        ),
                        new XElement("CorreoElectronico", factura.Receptor.CorreoElectronico)
                    ),
                    new XElement("CondicionVenta", "Contado"),
                    new XElement("PlazoCredito", "0"),
                    new XElement("MedioPago", "Efectivo"),
                    new XElement("DetalleServicio",
                        new XElement("LineaDetalle",
                            new XElement("NumeroLinea", "1"),
                            new XElement("Codigo",
                                new XElement("Tipo", "01")
                            ),
                            new XElement("Cantidad", "1"),
                            new XElement("UnidadMedida", "Unidad"),
                            new XElement("MontoTotal", factura.ResumenFactura.TotalComprobante.ToString("F2")),
                            new XElement("MontoDescuento", "0"),
                            new XElement("NaturalezaDescuento", "N/A"),
                            new XElement("SubTotal", factura.ResumenFactura.TotalGravado.ToString("F2")),
                            new XElement("Impuesto",
                                new XElement("Codigo", "01"), 
                                new XElement("Tarifa", "13"), 
                                new XElement("Monto", factura.ResumenFactura.TotalImpuesto.ToString("F2"))
                            ),
                            new XElement("MontoTotalLinea", factura.ResumenFactura.TotalImpuesto.ToString("F2"))
                        )
                    ),
                    new XElement("ResumenFactura",
                        new XElement("CodigoMoneda", "CRC"),
                        new XElement("TipoCambio", "1.00"),
                        new XElement("TotalServGravados", factura.ResumenFactura.TotalGravado.ToString("F2")),
                        new XElement("TotalServExentos", factura.ResumenFactura.TotalImpuesto.ToString("F2")),
                        new XElement("TotalMercaderiaGravada", factura.ResumenFactura.TotalGravado.ToString("F2")),
                        new XElement("TotalMercaderiaExenta", factura.ResumenFactura.TotalGravado.ToString("F2")),
                        new XElement("TotalGravado", factura.ResumenFactura.TotalGravado.ToString("F2")),
                        new XElement("TotalExento", factura.ResumenFactura.TotalGravado.ToString("F2")),
                        new XElement("TotalVenta", factura.ResumenFactura.TotalComprobante.ToString("F2")),
                        new XElement("TotalDescuentos", factura.ResumenFactura.TotalImpuesto.ToString("F2")),
                        new XElement("TotalVentaNeta", factura.ResumenFactura.TotalImpuesto.ToString("F2")),
                        new XElement("TotalImpuesto", factura.ResumenFactura.TotalImpuesto.ToString("F2")),
                        new XElement("TotalIVADevuelto", "0"),
                        new XElement("TotalMontoImpuesto", factura.ResumenFactura.TotalImpuesto.ToString("F2")),
                        new XElement("TotalFactura", factura.ResumenFactura.TotalImpuesto.ToString("F2"))
                    ),
                    new XElement("Otros",
                        new XElement("OtrosDatos", "Mundo Fantastico A.S")
                    )
                ));

            return doc.ToString();
        }

    }
}
