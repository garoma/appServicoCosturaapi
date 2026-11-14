
using appServicosCostura.Application.Autenticacao;

namespace appServicosCostura.Application.Interfaces;

public interface ILoginAdminService
{
    Task<LoginAdminResult?> LoginAsync(
        LoginAdminRequest request,
        CancellationToken cancellationToken = default);
}
