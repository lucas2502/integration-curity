using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Credential> Credentials => Set<Credential>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}