using RotasApi.DTOs;
using RotasApi.Models;

namespace RotasApi.Services;

public interface IRotaService
{
    Task<IEnumerable<Rota>> ObterTodas();
    Task<RotaDTO> ObterPorId(int id);
    Task<Rota> Adicionar(RotaDTO rotaDto);
    Task<RotaDTO> Atualizar(int id, RotaDTO rotaDto);
    Task<bool> Remover(int id);
    Task<string> ConsultarMelhorRota(string origem, string destino);
}
