
namespace appServicosCostura.Application.Autenticacao;

public sealed record LoginProfissionalResult(
    Guid ProfissionalId,
    string Token,
    DateTime ExpiraEmUtc,
    string Nome,
    string Telefone,
    string Email,
    string Perfil,
    string? Foto,
    string? FotoContentType);
