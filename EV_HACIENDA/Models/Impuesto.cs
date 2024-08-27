using System.ComponentModel.DataAnnotations;

namespace EV_HACIENDA.Models
{
    public class Impuesto
    {
        [Key]
        public int ImpuestoId { get; set; } 
        public decimal Tarifa { get; set; }
        public decimal Monto { get; set; }

    }
}
