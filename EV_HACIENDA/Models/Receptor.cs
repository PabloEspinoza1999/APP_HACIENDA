using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EV_HACIENDA.Models
{
    public class Receptor
    {
        [Key]
        public int ReceptorId { get; set; } 
        public string Nombre { get; set; }

        public string CorreoElectronico { get; set; }

        public string Telefono { get; set; }

        public  string Identificacion { get; set; }
        public virtual ICollection<FacturaElectronica> FacturasElectronicas { get; set; }

    }
}

