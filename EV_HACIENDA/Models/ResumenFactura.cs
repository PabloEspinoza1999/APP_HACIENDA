using System.ComponentModel.DataAnnotations;

namespace EV_HACIENDA.Models
{
    public class ResumenFactura
    {
        [Key]
        public int ResumenFacturaId { get; set; } 
        public decimal TotalGravado { get; set; }
        public decimal TotalImpuesto { get; set; }
        public decimal TotalComprobante { get; set; }
        public virtual ICollection<FacturaElectronica> FacturasElectronicas { get; set; }

    }
}
