
using MediatR;

namespace appServicosCostura.Application.Clientes.Commands;

public sealed record AtualizarClienteCommand(
    Guid ClienteId,
    string Nome,
    string Telefone,
    string? Email,
    string Endereco,
    string Bairro,
    string Cidade,
    string Cep,
    string? PontoReferencia,
    byte[]? Foto,
    string? FotoContentType,
    bool RemoverFoto = false
) : IRequest<bool>;
