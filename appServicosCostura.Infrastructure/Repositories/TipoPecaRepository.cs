
using appServicosCostura.Application.Interfaces;
using appServicosCostura.Domain.Entities;
using appServicosCostura.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace appServicosCostura.Infrastructure.Repositories;

public sealed class TipoPecaRepository : ITipoPecaRepository
{
    private readonly appServicosCosturaDbContext _context;

    public TipoPecaRepository(appServicosCosturaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(
        TipoPeca tipoPeca,
        CancellationToken cancellationToken = default)
    {
        await _context.TiposPecas.AddAsync(
            tipoPeca, cancellationToken);
    }

    public Task SalvarAlteracoesAsync(
        CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }

    public Task<TipoPeca?> ObterPorIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _context.TiposPecas
            .FirstOrDefaultAsync(t => t.Id == id, cancellationToken);
    }

    public Task<bool> NomeExisteAsync(
        string nome,
        Guid? idIgnorado = null,
        CancellationToken cancellationToken = default)
    {
        var nomeNormalizado = nome.Trim();

        return _context.TiposPecas.AnyAsync(
            t => t.Nome == nomeNormalizado &&
                 (!idIgnorado.HasValue || t.Id != idIgnorado.Value),
            cancellationToken);
    }

    public async Task<IReadOnlyList<TipoPeca>> ListarTodosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.TiposPecas
            .AsNoTracking()
            .OrderBy(t => t.Nome)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<TipoPeca>> ListarAtivosAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.TiposPecas
            .AsNoTracking()
            .Where(t => t.Ativo)
            .OrderBy(t => t.Nome)
            .ToListAsync(cancellationToken);
    }
}
