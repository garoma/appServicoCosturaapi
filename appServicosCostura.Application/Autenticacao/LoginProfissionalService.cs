using appServicosCostura.Application.Autenticacao;
using appServicosCostura.Application.Interfaces;
using appServicosCostura.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace appServicosCostura.Infrastructure.Services;

public sealed class LoginProfissionalService(
    IProfissionalRepository profissionalRepository,
    IPasswordHasher<Profissional> passwordHasher,
    ITokenService tokenService)
    : ILoginProfissionalService
{
    public async Task<LoginProfissionalResult?> LoginAsync(
        LoginProfissionalRequest request,
        CancellationToken cancellationToken = default)
    {
        string identificador = request.Identificador.Trim();

        Profissional? profissional;

        if (identificador.Contains('@'))
        {
            profissional = await profissionalRepository.ObterPorEmailAsync(
                identificador,
                cancellationToken);
        }
        else
        {
            profissional = await profissionalRepository.ObterPorTelefoneAsync(
                identificador,
                cancellationToken);
        }

        if (profissional is null ||
            string.IsNullOrWhiteSpace(profissional.SenhaHash) ||
            profissional.Status != "Ativo")
        {
            return null;
        }

        var resultado = passwordHasher.VerifyHashedPassword(
            profissional,
            profissional.SenhaHash,
            request.Senha);

        if (resultado == PasswordVerificationResult.Failed)
            return null;

        var usuario = new UsuarioAutenticavel(
            profissional.Id,
            profissional.Nome,
            profissional.Email ?? string.Empty,
            "Profissional");

        var (token, expiraEmUtc) = tokenService.GerarToken(usuario);

        return new LoginProfissionalResult(
            profissional.Id,
            token,
            expiraEmUtc,
            profissional.Nome,
            profissional.Telefone,
            profissional.Email ?? string.Empty,
            profissional.Categoria,
            "profissional");
    }
}