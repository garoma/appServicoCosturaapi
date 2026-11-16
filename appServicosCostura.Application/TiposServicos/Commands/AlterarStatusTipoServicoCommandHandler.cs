
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.TiposServicos.Commands;

public sealed class AlterarStatusTipoServicoCommandHandler
    : IRequestHandler<AlterarStatusTipoServicoCommand>
{
    private readonly ITipoServicoRepository _repository;

    public AlterarStatusTipoServicoCommandHandler(
        ITipoServicoRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        AlterarStatusTipoServicoCommand request,
        CancellationToken cancellationToken)
    {
        var tipo = await _repository.ObterPorIdAsync(
            request.Id, cancellationToken);

        if (tipo is null)
            throw new KeyNotFoundException("Tipo de serviço não encontrado.");

        tipo.AlterarStatus(request.Ativo);

        await _repository.SalvarAlteracoesAsync(cancellationToken);
    }
}
