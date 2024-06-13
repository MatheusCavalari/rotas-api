using Microsoft.EntityFrameworkCore;
using RotasApi.Models;

namespace RotasApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Rota> Rotas { get; set; }
    }
}
