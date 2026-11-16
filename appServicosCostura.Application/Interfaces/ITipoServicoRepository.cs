
using appServicosCostura.Domain.Entities;

namespace appServicosCostura.Application.Interfaces;

public interface ITipoServicoRepository
{
    Task AdicionarAsync(
        TipoServico tipoServico,
        CancellationToken cancellationToken = default);

    Task SalvarAlteracoesAsync(
        CancellationToken cancellationToken = default);

    Task<TipoServico?> ObterPorIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<bool> NomeExisteAsync(
        string nome,
        Guid? idIgnorado = null,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TipoServico>> ListarTodosAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TipoServico>> ListarAtivosAsync(
        CancellationToken cancellationToken = default);
}
