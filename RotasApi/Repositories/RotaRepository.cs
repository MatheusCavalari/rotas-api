using Microsoft.EntityFrameworkCore;
using RotasApi.Data;
using RotasApi.Models;

namespace RotasApi.Repositories;

public class RotaRepository : IRotaRepository
{
    private readonly ApplicationDbContext _context;

    public RotaRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Rota>> ObterTodas()
    {
        return await _context.Rotas.ToListAsync();
    }

    public async Task<Rota> ObterPorId(int id)
    {
        return await _context.Rotas.FindAsync(id);
    }

    public async Task<Rota> Adicionar(Rota rota)
    {
        _context.Rotas.Add(rota);
        await _context.SaveChangesAsync();
        return rota;
    }

    public async Task<Rota> Atualizar(Rota rota)
    {
        _context.Entry(rota).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return rota;
    }

    public async Task<bool> Remover(int id)
    {
        var rota = await _context.Rotas.FindAsync(id);
        if (rota == null)
        {
            return false;
        }

        _context.Rotas.Remove(rota);
        await _context.SaveChangesAsync();
        return true;
    }
}
