
using appServicosCostura.Domain.Entities;

namespace appServicosCostura.Application.Interfaces;

public interface IProfissionalRepository
{
    Task AdicionarAsync(
        Profissional profissional,
        CancellationToken cancellationToken = default);

    Task SalvarAlteracoesAsync(
        CancellationToken cancellationToken = default);

    Task<Profissional?> ObterPorTelefoneAsync(
        string telefone,
        CancellationToken cancellationToken = default);

    Task<Profissional?> ObterPorEmailAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task<Profissional?> ObterPorIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<bool> TelefoneExisteParaOutroProfissionalAsync(
        string telefone,
        Guid profissionalId,
        CancellationToken cancellationToken = default);

    Task<bool> EmailExisteParaOutroProfissionalAsync(
        string email,
        Guid profissionalId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Profissional>> ListarTodosAsync(
        CancellationToken cancellationToken = default);
}
