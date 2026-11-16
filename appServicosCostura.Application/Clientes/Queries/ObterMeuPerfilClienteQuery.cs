
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.Clientes.Queries;

public sealed record ObterMeuPerfilClienteQuery(Guid ClienteId)
    : IRequest<ObterMeuPerfilClienteResult?>;

public sealed record ObterMeuPerfilClienteResult(
    Guid Id,
    string Nome,
    string Telefone,
    string? Email,
    string Endereco,
    string Bairro,
    string Cidade,
    string Cep,
    string? PontoReferencia,
    string? Foto,
    string? FotoContentType
);

public sealed class ObterMeuPerfilClienteQueryHandler
    : IRequestHandler<
        ObterMeuPerfilClienteQuery,
        ObterMeuPerfilClienteResult?>
{
    private readonly IClienteRepository _repository;

    public ObterMeuPerfilClienteQueryHandler(
        IClienteRepository repository)
    {
        _repository = repository;
    }

    public async Task<ObterMeuPerfilClienteResult?> Handle(
        ObterMeuPerfilClienteQuery request,
        CancellationToken cancellationToken)
    {
        var cliente = await _repository.ObterPorIdAsync(
            request.ClienteId,
            cancellationToken);

        if (cliente is null)
            return null;

        var foto = cliente.Foto is { Length: > 0 }
            ? Convert.ToBase64String(cliente.Foto)
            : null;

        return new ObterMeuPerfilClienteResult(
            cliente.Id,
            cliente.Nome,
            cliente.Telefone,
            cliente.Email,
            cliente.Endereco,
            cliente.Bairro,
            cliente.Cidade,
            cliente.Cep,
            cliente.PontoReferencia,
            foto,
            cliente.FotoContentType
        );
    }
}
