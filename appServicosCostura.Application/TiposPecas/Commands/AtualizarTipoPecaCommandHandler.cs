
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.TiposPecas.Commands;

public sealed class AtualizarTipoPecaCommandHandler
    : IRequestHandler<AtualizarTipoPecaCommand>
{
    private readonly ITipoPecaRepository _repository;

    public AtualizarTipoPecaCommandHandler(
        ITipoPecaRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(
        AtualizarTipoPecaCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("O nome da peça é obrigatório.");

        var tipo = await _repository.ObterPorIdAsync(
            request.Id, cancellationToken);

        if (tipo is null)
            throw new KeyNotFoundException("Tipo de peça não encontrado.");

        var nome = request.Nome.Trim();

        if (await _repository.NomeExisteAsync(
            nome, request.Id, cancellationToken))
        {
            throw new ArgumentException(
                "Já existe outro tipo de peça com esse nome.");
        }

        tipo.Atualizar(nome);

        await _repository.SalvarAlteracoesAsync(cancellationToken);
    }
}
