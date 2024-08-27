using System.ComponentModel.DataAnnotations;
namespace EV_HACIENDA.Models
{
    public class Codigo
    {
        [Key]
        public int CodigoId { get; set; }
        public string Tipo { get; set; }
        public string CodigoValor { get; set; }
    }
}
