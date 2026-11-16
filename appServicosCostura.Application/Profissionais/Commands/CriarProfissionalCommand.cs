using MediatR;

namespace appServicosCostura.Application.Profissionais.Commands;

public sealed record CriarProfissionalCommand(
    string Nome,
    string Telefone,
    string? Email,
    string Senha,
    string Endereco,
    string Bairro,
    string Cidade,
    string Cep,
    string? PontoReferencia,
    List<string> Servicos,
    List<string> Pecas,
    byte[]? Foto = null,
    string? FotoContentType = null
) : IRequest<Guid>;