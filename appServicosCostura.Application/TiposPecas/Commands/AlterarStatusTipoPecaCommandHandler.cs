
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.TiposPecas.Commands;

public sealed class AlterarStatusTipoPecaCommandHandler
    : IRequestHandler<AlterarStatusTipoPecaCommand>
{
    private readonly ITipoPecaRepository _repository;

    public AlterarStatusTipoPecaCommandHandler(
        ITipoPecaRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        AlterarStatusTipoPecaCommand request,
        CancellationToken cancellationToken)
    {
        var tipo = await _repository.ObterPorIdAsync(
            request.Id, cancellationToken);

        if (tipo is null)
            throw new KeyNotFoundException("Tipo de peça não encontrado.");

        tipo.AlterarStatus(request.Ativo);

        await _repository.SalvarAlteracoesAsync(cancellationToken);
    }
}
