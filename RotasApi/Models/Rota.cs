using System.ComponentModel.DataAnnotations;

namespace RotasApi.Models
{
    public class Rota
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        
        public int Valor { get; set; }
    }
}
