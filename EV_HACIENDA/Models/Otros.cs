using System.ComponentModel.DataAnnotations;

namespace EV_HACIENDA.Models
{
    public class Otros
    {
        [Key]
        public int OtrosId { get; set; }
        public string OtrosDatos { get; set; }
    }
}
