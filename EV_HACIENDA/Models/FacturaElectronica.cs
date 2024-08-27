using EV_HACIENDA.Models;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EV_HACIENDA.Models
{
    public class FacturaElectronica
    {
        [Key]
        public int FacturaElectronicaId { get; set; }
        public string Clave { get; set; }
        public string NumeroConsecutivo { get; set; }
        public DateTime FechaEmision { get; set; }
        public int EmisorId { get; set; }
        public int ReceptorId { get; set; }
        public int ResumenFacturaId { get; set; }
        public string EstadoEnvio { get; set; }

        [ForeignKey(nameof(EmisorId))]
        public virtual Emisor Emisor { get; set; }
        [ForeignKey(nameof(ReceptorId))]
        public virtual Receptor Receptor { get; set; }
        [ForeignKey(nameof(ResumenFacturaId))]
        public virtual ResumenFactura ResumenFactura { get; set; }
        public virtual List<LineaDetalle> LineasDetalles { get; set; }
    }
}
