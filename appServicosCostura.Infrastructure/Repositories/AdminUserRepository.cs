
using appServicosCostura.Application.Interfaces;
using appServicosCostura.Domain.Entities;
using appServicosCostura.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace appServicosCostura.Infrastructure.Repositories;

public sealed class AdminUserRepository(
    appServicosCosturaDbContext context) : IAdminUserRepository
{
    public Task<AdminUser?> ObterPorEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var emailNormalizado = email.Trim().ToLowerInvariant();

        return context.AdminUsers
            .SingleOrDefaultAsync(
                x => x.Email == emailNormalizado,
                cancellationToken);
    }

    public async Task AdicionarAsync(
        AdminUser admin,
        CancellationToken cancellationToken = default)
    {
        await context.AdminUsers.AddAsync(admin, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}
