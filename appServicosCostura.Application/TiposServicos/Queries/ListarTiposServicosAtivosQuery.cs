
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.TiposServicos.Queries;

public sealed record ListarTiposServicosAtivosQuery
    : IRequest<IReadOnlyList<ListarTiposServicosAtivosResult>>;

public sealed record ListarTiposServicosAtivosResult(
    Guid Id,
    string Nome
);

public sealed class ListarTiposServicosAtivosQueryHandler
    : IRequestHandler<
        ListarTiposServicosAtivosQuery,
        IReadOnlyList<ListarTiposServicosAtivosResult>>
{
    private readonly ITipoServicoRepository _repository;

    public ListarTiposServicosAtivosQueryHandler(
        ITipoServicoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ListarTiposServicosAtivosResult>> Handle(
        ListarTiposServicosAtivosQuery request,
        CancellationToken cancellationToken)
    {
        var tipos = await _repository.ListarAtivosAsync(
            cancellationToken);

        return tipos.Select(tipo => new ListarTiposServicosAtivosResult(
            tipo.Id,
            tipo.Nome
        )).ToList();
    }
}
