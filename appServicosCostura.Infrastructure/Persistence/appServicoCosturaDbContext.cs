
using appServicosCostura.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace appServicosCostura.Infrastructure.Persistence;

public sealed class appServicosCosturaDbContext(
    DbContextOptions<appServicosCosturaDbContext> options)
    : DbContext(options)
{
    public DbSet<AdminUser> AdminUsers => Set<AdminUser>();

    public DbSet<Cliente> Clientes => Set<Cliente>();
    public DbSet<Profissional> Profissionais => Set<Profissional>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(appServicosCosturaDbContext).Assembly);
    }
}
