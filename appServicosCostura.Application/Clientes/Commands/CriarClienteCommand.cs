
using MediatR;

namespace appServicosCostura.Application.Clientes.Commands;

public sealed record CriarClienteCommand(
    string Nome,
    string Telefone,
    string? Email,
    string Senha,
    string? Endereco = null,
    string? Bairro = null,
    string? Cidade = null,
    string? Cep = null,
    string? PontoReferencia = null
) : IRequest<Guid>;
