
using appServicosCostura.Domain.Entities;

namespace appServicosCostura.Application.Interfaces;

public interface ITipoPecaRepository
{
    Task AdicionarAsync(
        TipoPeca tipoPeca,
        CancellationToken cancellationToken = default);

    Task SalvarAlteracoesAsync(
        CancellationToken cancellationToken = default);

    Task<TipoPeca?> ObterPorIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<bool> NomeExisteAsync(
        string nome,
        Guid? idIgnorado = null,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TipoPeca>> ListarTodosAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<TipoPeca>> ListarAtivosAsync(
        CancellationToken cancellationToken = default);
}
