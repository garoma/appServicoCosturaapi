
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.TiposServicos.Queries;

public sealed record ListarTiposServicosQuery
    : IRequest<IReadOnlyList<ListarTiposServicosResult>>;

public sealed record ListarTiposServicosResult(
    Guid Id,
    string Nome,
    bool Ativo,
    DateTime CriadoEmUtc
);

public sealed class ListarTiposServicosQueryHandler
    : IRequestHandler<
        ListarTiposServicosQuery,
        IReadOnlyList<ListarTiposServicosResult>>
{
    private readonly ITipoServicoRepository _repository;

    public ListarTiposServicosQueryHandler(
        ITipoServicoRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ListarTiposServicosResult>> Handle(
        ListarTiposServicosQuery request,
        CancellationToken cancellationToken)
    {
        var tipos = await _repository.ListarTodosAsync(
            cancellationToken);

        return tipos.Select(tipo => new ListarTiposServicosResult(
            tipo.Id,
            tipo.Nome,
            tipo.Ativo,
            tipo.CriadoEmUtc
        )).ToList();
    }
}
