using System.ComponentModel.DataAnnotations;

namespace RotasApi.Models
{
    public class Rota
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "A origem da rota é obrigaatória")]
        public string Origem { get; set; }
        [Required(ErrorMessage = "O destino da rota é obrigaatório")]
        public string Destino { get; set; }
        [Required(ErrorMessage = "O valor da rota é obrigaatória")]
        [MinLength(0, ErrorMessage = "O valor mínimo para a rota é 0")]
        public decimal Valor { get; set; }
    }
}
