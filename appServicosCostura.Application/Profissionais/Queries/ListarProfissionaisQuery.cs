
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.Profissionais.Queries;

public sealed record ListarProfissionaisQuery
    : IRequest<IReadOnlyList<ListarProfissionaisResult>>;

public sealed record ListarProfissionaisResult(
    Guid Id,
    string Nome,
    string Telefone,
    string? Email,
    string Status,
    string Bairro,
    string Cidade,
    string Categoria,
    decimal PrecoBase,
    List<string> Servicos,
    List<string> Pecas
);

public sealed class ListarProfissionaisQueryHandler
    : IRequestHandler<ListarProfissionaisQuery,
        IReadOnlyList<ListarProfissionaisResult>>
{
    private readonly IProfissionalRepository _profissionalRepository;

    public ListarProfissionaisQueryHandler(
        IProfissionalRepository profissionalRepository)
    {
        _profissionalRepository = profissionalRepository;
    }

    public async Task<IReadOnlyList<ListarProfissionaisResult>> Handle(
        ListarProfissionaisQuery request,
        CancellationToken cancellationToken)
    {
        var profissionais =
            await _profissionalRepository.ListarTodosAsync(
                cancellationToken);

        return profissionais.Select(profissional =>
            new ListarProfissionaisResult(
                profissional.Id,
                profissional.Nome,
                profissional.Telefone,
                profissional.Email,
                profissional.Status,
                profissional.Bairro,
                profissional.Cidade,
                profissional.Categoria,
                profissional.PrecoBase,
                profissional.Servicos,
                profissional.Pecas
            )).ToList();
    }
}
