using System.ComponentModel.DataAnnotations;

namespace RotasApi.DTOs
{
    public class RotaDTO
    {
        [Required(ErrorMessage = "A origem da rota é obrigaatória")]
        public string Origem { get; set; }
        [Required(ErrorMessage = "O destino da rota é obrigaatório")]
        public string Destino { get; set; }
        [Required(ErrorMessage = "O valor da rota é obrigaatória")]
        [Range(0, int.MaxValue, ErrorMessage = "O valor mínimo para a rota é 0")]
        public int Valor { get; set; }
    }
}
