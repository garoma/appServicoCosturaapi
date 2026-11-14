
using appServicosCostura.Application.Interfaces;
using appServicosCostura.Domain.Entities;
using appServicosCostura.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace appServicosCostura.Infrastructure.Repositories;

public sealed class ProfissionalRepository : IProfissionalRepository
{
    private readonly appServicosCosturaDbContext _context;

    public ProfissionalRepository(appServicosCosturaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(
        Profissional profissional,
        CancellationToken cancellationToken = default)
    {
        await _context.Profissionais.AddAsync(
            profissional,
            cancellationToken);
    }

    public Task SalvarAlteracoesAsync(
        CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task<Profissional?> ObterPorTelefoneAsync(
        string telefone,
        CancellationToken cancellationToken = default)
    {
        var telefoneNormalizado = telefone.Trim();

        return _context.Profissionais
            .FirstOrDefaultAsync(
                p => p.Telefone == telefoneNormalizado,
                cancellationToken);
    }

    public Task<Profissional?> ObterPorEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var emailNormalizado = email.Trim();

        return _context.Profissionais
            .FirstOrDefaultAsync(
                p => p.Email == emailNormalizado,
                cancellationToken);
    }

    public Task<Profissional?> ObterPorIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _context.Profissionais
            .FirstOrDefaultAsync(
                p => p.Id == id,
                cancellationToken);
    }

    public Task<bool> TelefoneExisteParaOutroProfissionalAsync(
        string telefone,
        Guid profissionalId,
        CancellationToken cancellationToken = default)
    {
        var telefoneNormalizado = telefone.Trim();

        return _context.Profissionais.AnyAsync(
            p => p.Telefone == telefoneNormalizado &&
                 p.Id != profissionalId,
            cancellationToken);
    }

    public Task<bool> EmailExisteParaOutroProfissionalAsync(
        string email,
        Guid profissionalId,
        CancellationToken cancellationToken = default)
    {
        var emailNormalizado = email.Trim();

        return _context.Profissionais.AnyAsync(
            p => p.Email == emailNormalizado &&
                 p.Id != profissionalId,
            cancellationToken);
    }

    public async Task<IReadOnlyList<Profissional>> ListarTodosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Profissionais
            .AsNoTracking()
            .OrderBy(p => p.Nome)
            .ToListAsync(cancellationToken);
    }
}
