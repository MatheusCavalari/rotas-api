using RotasApi.DTOs;

namespace RotasApi.Services;

public interface IRotaService
{
    Task<IEnumerable<RotaDTO>> ObterTodas();
    Task<RotaDTO> ObterPorId(int id);
    Task<RotaDTO> Adicionar(RotaDTO rotaDto);
    Task<RotaDTO> Atualizar(int id, RotaDTO rotaDto);
    Task<bool> Remover(int id);
    Task<string> ConsultarMelhorRota(string origem, string destino);
}
