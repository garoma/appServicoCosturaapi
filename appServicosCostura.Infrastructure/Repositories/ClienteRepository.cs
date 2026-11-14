
using appServicosCostura.Application.Interfaces;
using appServicosCostura.Domain.Entities;
using appServicosCostura.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace appServicosCostura.Infrastructure.Repositories;

public sealed class ClienteRepository : IClienteRepository
{
    private readonly appServicosCosturaDbContext _context;

    public ClienteRepository(appServicosCosturaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(
        Cliente cliente,
        CancellationToken cancellationToken = default)
    {
        await _context.Clientes.AddAsync(
            cliente,
            cancellationToken);
    }

    public Task SalvarAlteracoesAsync(
        CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }


    public Task<Cliente?> ObterPorTelefoneAsync(
        string telefone,
        CancellationToken cancellationToken = default)
    {
        var telefoneNormalizado = telefone.Trim();

        return _context.Clientes
            .FirstOrDefaultAsync(
                p => p.Telefone == telefoneNormalizado,
                cancellationToken);
    }

    public Task<Cliente?> ObterPorEmailAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var emailNormalizado = email.Trim();

        return _context.Clientes
            .FirstOrDefaultAsync(
                p => p.Email == emailNormalizado,
                cancellationToken);
    }


    public Task<Cliente?> ObterPorIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return _context.Clientes
            .FirstOrDefaultAsync(
                p => p.Id == id,
                cancellationToken);
    }

    public Task<bool> TelefoneExisteParaOutroClienteAsync(
    string telefone,
    Guid clienteId,
    CancellationToken cancellationToken = default)
    {
        var telefoneNormalizado = telefone.Trim();

        return _context.Clientes.AnyAsync(
            p => p.Telefone == telefoneNormalizado &&
                 p.Id != clienteId,
            cancellationToken);
    }

    public Task<bool> EmailExisteParaOutroClienteAsync(
        string email,
        Guid clienteId,
        CancellationToken cancellationToken = default)
    {
        var emailNormalizado = email.Trim();

        return _context.Clientes.AnyAsync(
            p => p.Email == emailNormalizado &&
                 p.Id != clienteId,
            cancellationToken);
    }

    public async Task<IReadOnlyList<Cliente>> ListarTodosAsync(
    CancellationToken cancellationToken = default)
    {
        return await _context.Clientes
            .AsNoTracking()
            .OrderBy(p => p.Nome)
            .ToListAsync(cancellationToken);
    }
}
