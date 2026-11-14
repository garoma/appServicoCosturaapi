using appServicosCostura.Application.Autenticacao;

namespace appServicosCostura.Application.Interfaces;

public interface ILoginProfissionalService
{
    Task<LoginProfissionalResult?> LoginAsync(
        LoginProfissionalRequest request,
        CancellationToken cancellationToken = default);
}