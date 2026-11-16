
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.TiposPecas.Queries;

public sealed record ListarTiposPecasQuery
    : IRequest<IReadOnlyList<ListarTiposPecasResult>>;

public sealed record ListarTiposPecasResult(
    Guid Id,
    string Nome,
    bool Ativo,
    DateTime CriadoEmUtc
);

public sealed class ListarTiposPecasQueryHandler
    : IRequestHandler<
        ListarTiposPecasQuery,
        IReadOnlyList<ListarTiposPecasResult>>
{
    private readonly ITipoPecaRepository _repository;

    public ListarTiposPecasQueryHandler(
        ITipoPecaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ListarTiposPecasResult>> Handle(
        ListarTiposPecasQuery request,
        CancellationToken cancellationToken)
    {
        var tipos = await _repository.ListarTodosAsync(
            cancellationToken);

        return tipos.Select(tipo => new ListarTiposPecasResult(
            tipo.Id,
            tipo.Nome,
            tipo.Ativo,
            tipo.CriadoEmUtc
        )).ToList();
    }
}
