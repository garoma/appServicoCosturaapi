
using appServicosCostura.Application.Autenticacao;

namespace appServicosCostura.Application.Interfaces;

public interface ILoginClienteService
{
    Task<LoginClienteResult?> LoginAsync(
        LoginClienteRequest request,
        CancellationToken cancellationToken = default);
}
