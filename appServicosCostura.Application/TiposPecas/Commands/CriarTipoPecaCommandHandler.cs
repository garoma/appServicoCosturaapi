
using appServicosCostura.Application.Interfaces;
using appServicosCostura.Domain.Entities;
using MediatR;

namespace appServicosCostura.Application.TiposPecas.Commands;

public sealed class CriarTipoPecaCommandHandler
    : IRequestHandler<CriarTipoPecaCommand, Guid>
{
    private readonly ITipoPecaRepository _repository;

    public CriarTipoPecaCommandHandler(
        ITipoPecaRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CriarTipoPecaCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("O nome da peça é obrigatório.");

        var nome = request.Nome.Trim();

        if (await _repository.NomeExisteAsync(
            nome, cancellationToken: cancellationToken))
        {
            throw new ArgumentException(
                "Este tipo de peça já está cadastrado.");
        }

        var tipoPeca = new TipoPeca(nome);

        await _repository.AdicionarAsync(
            tipoPeca, cancellationToken);

        await _repository.SalvarAlteracoesAsync(cancellationToken);

        return tipoPeca.Id;
    }
}
