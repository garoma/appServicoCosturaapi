
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.TiposPecas.Queries;

public sealed record ListarTiposPecasAtivosQuery
    : IRequest<IReadOnlyList<ListarTiposPecasAtivosResult>>;

public sealed record ListarTiposPecasAtivosResult(
    Guid Id,
    string Nome
);

public sealed class ListarTiposPecasAtivosQueryHandler
    : IRequestHandler<
        ListarTiposPecasAtivosQuery,
        IReadOnlyList<ListarTiposPecasAtivosResult>>
{
    private readonly ITipoPecaRepository _repository;

    public ListarTiposPecasAtivosQueryHandler(
        ITipoPecaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ListarTiposPecasAtivosResult>> Handle(
        ListarTiposPecasAtivosQuery request,
        CancellationToken cancellationToken)
    {
        var tipos = await _repository.ListarAtivosAsync(
            cancellationToken);

        return tipos.Select(tipo => new ListarTiposPecasAtivosResult(
            tipo.Id,
            tipo.Nome
        )).ToList();
    }
}
