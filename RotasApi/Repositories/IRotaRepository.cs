using RotasApi.Models;

namespace RotasApi.Repositories;

public interface IRotaRepository
{
    Task<IEnumerable<Rota>> ObterTodas();
    Task<Rota> ObterPorId(int id);
    Task<Rota> Adicionar(Rota rota);
    Task<Rota> Atualizar(Rota rota);
    Task<bool> Remover(int id);
}
