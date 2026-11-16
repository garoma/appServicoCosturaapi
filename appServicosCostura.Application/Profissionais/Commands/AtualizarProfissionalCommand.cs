
using MediatR;

namespace appServicosCostura.Application.Profissionais.Commands;

public sealed record AtualizarProfissionalCommand(
    Guid ProfissionalId,
    string Nome,
    string Telefone,
    string? Email,
    string Endereco,
    string Bairro,
    string Cidade,
    string Cep,
    string? PontoReferencia,
    List<string> Servicos,
    List<string> Pecas,
    byte[]? Foto,
    string? FotoContentType,
    bool RemoverFoto = false
) : IRequest<bool>;
