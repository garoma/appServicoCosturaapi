
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.TiposServicos.Commands;

public sealed class AtualizarTipoServicoCommandHandler
    : IRequestHandler<AtualizarTipoServicoCommand>
{
    private readonly ITipoServicoRepository _repository;

    public AtualizarTipoServicoCommandHandler(
        ITipoServicoRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        AtualizarTipoServicoCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("O nome do serviço é obrigatório.");

        var tipo = await _repository.ObterPorIdAsync(
            request.Id, cancellationToken);

        if (tipo is null)
            throw new KeyNotFoundException("Tipo de serviço não encontrado.");

        var nome = request.Nome.Trim();

        if (await _repository.NomeExisteAsync(
            nome, request.Id, cancellationToken))
        {
            throw new ArgumentException(
                "Já existe outro tipo de serviço com esse nome.");
        }

        tipo.Atualizar(nome);

        await _repository.SalvarAlteracoesAsync(cancellationToken);
    }
}
