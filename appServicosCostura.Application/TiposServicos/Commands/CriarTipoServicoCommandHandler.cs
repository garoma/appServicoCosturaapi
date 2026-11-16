
using appServicosCostura.Application.Interfaces;
using appServicosCostura.Domain.Entities;
using MediatR;

namespace appServicosCostura.Application.TiposServicos.Commands;

public sealed class CriarTipoServicoCommandHandler
    : IRequestHandler<CriarTipoServicoCommand, Guid>
{
    private readonly ITipoServicoRepository _repository;

    public CriarTipoServicoCommandHandler(
        ITipoServicoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        CriarTipoServicoCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("O nome do serviço é obrigatório.");

        var nome = request.Nome.Trim();

        if (await _repository.NomeExisteAsync(
            nome, cancellationToken: cancellationToken))
        {
            throw new ArgumentException(
                "Este tipo de serviço já está cadastrado.");
        }

        var tipoServico = new TipoServico(nome);

        await _repository.AdicionarAsync(
            tipoServico, cancellationToken);

        await _repository.SalvarAlteracoesAsync(cancellationToken);

        return tipoServico.Id;
    }
}
