
namespace appServicosCostura.Application.Interfaces;

public interface ITokenService
{
    (string Token, DateTime ExpiraEmUtc) GerarToken(
        UsuarioAutenticavel usuario);
}
