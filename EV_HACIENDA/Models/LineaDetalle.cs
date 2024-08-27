using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EV_HACIENDA.Models
{
    public class LineaDetalle
    {
        [Key]
        public int LineaDetalleId { get; set; } 
        public string Detalle { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal MontoTotal { get; set; }
        public decimal Cantidad { get; set; }
        public string UnidadMedida { get; set; }
        public decimal MontoDescuento { get; set; }
        public string NaturalezaDescuento { get; set; }
        public decimal SubTotal { get; set; }
        public decimal MontoTotalLinea { get; set; }

       
        public int? ImpuestoId { get; set; }
        [ForeignKey(nameof(ImpuestoId))]
        public virtual Impuesto Impuesto { get; set; }

        public int FacturaElectronicaId { get; set; }
        [ForeignKey(nameof(FacturaElectronicaId))]
        public virtual FacturaElectronica FacturaElectronica { get; set; }

        public int CodigoId { get; set; }
        [ForeignKey(nameof(CodigoId))]
        public virtual Codigo Codigo { get; set; }

    }
}
