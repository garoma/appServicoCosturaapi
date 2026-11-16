
using appServicosCostura.Application.Interfaces;
using appServicosCostura.Domain.Entities;
using appServicosCostura.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace appServicosCostura.Infrastructure.Repositories;

public sealed class TipoServicoRepository : ITipoServicoRepository
{
    private readonly appServicosCosturaDbContext _context;

    public TipoServicoRepository(appServicosCosturaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(
        TipoServico tipoServico,
        CancellationToken cancellationToken = default)
    {
        await _context.TiposServicos.AddAsync(
            tipoServico, cancellationToken);
    }

    public Task SalvarAlteracoesAsync(
        CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task<TipoServico?> ObterPorIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _context.TiposServicos
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public Task<bool> NomeExisteAsync(
        string nome,
        Guid? idIgnorado = null,
        CancellationToken cancellationToken = default)
    {
        var nomeNormalizado = nome.Trim();

        return _context.TiposServicos.AnyAsync(
            t => t.Nome == nomeNormalizado &&
                 (!idIgnorado.HasValue || t.Id != idIgnorado.Value),
            cancellationToken);
    }

    public async Task<IReadOnlyList<TipoServico>> ListarTodosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.TiposServicos
            .AsNoTracking()
            .OrderBy(t => t.Nome)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TipoServico>> ListarAtivosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.TiposServicos
            .AsNoTracking()
            .Where(t => t.Ativo)
            .OrderBy(t => t.Nome)
            .ToListAsync(cancellationToken);
    }
}
