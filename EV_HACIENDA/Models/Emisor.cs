using System.ComponentModel.DataAnnotations;

namespace EV_HACIENDA.Models
{
    public class Emisor
    {
        [Key]
        public int EmisorId { get; set; } 
        public string Nombre { get; set; }
        public string Identificacion { get; set; }
        public string CorreoElectronico { get; set; }
        public string Provincia { get; set; }
        public string Telefono { get; set; }
        public string Canton { get; set; }
        public string Distrito { get; set; }
        public string Barrio { get; set; }
        public string OtrasSenas { get; set; }
        public string CodigoPais { get; set; }
        public virtual ICollection<FacturaElectronica> FacturasElectronicas { get; set; }

    }
}
