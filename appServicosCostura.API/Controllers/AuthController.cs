using appServicosCostura.Application.Autenticacao;
using appServicosCostura.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace appServicosCostura.API.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(
    ILoginAdminService loginAdminService,
    ILoginClienteService loginClienteService,
    ILoginProfissionalService loginProfissionalService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginAdminRequest request,
        CancellationToken cancellationToken)
    {
        var resultado = await loginAdminService.LoginAsync(
            request,
            cancellationToken);

        if (resultado is null)
        {
            return Unauthorized(new
            {
                mensagem = "E-mail ou senha inválidos."
            });
        }

        return Ok(resultado);
    }

    [AllowAnonymous]
    [HttpPost("cliente/login")]
    public async Task<IActionResult> LoginCliente(
        [FromBody] LoginClienteRequest request,
        CancellationToken cancellationToken)
    {
        var resultado = await loginClienteService.LoginAsync(
            request,
            cancellationToken);

        if (resultado is null)
        {
            return Unauthorized(new
            {
                mensagem = "Telefone/E-mail ou senha inválidos."
            });
        }

        return Ok(resultado);
    }

    [AllowAnonymous]
    [HttpPost("profissional/login")]
    public async Task<IActionResult> LoginProfissional(
        [FromBody] LoginProfissionalRequest request,
        CancellationToken cancellationToken)
    {
        var resultado = await loginProfissionalService.LoginAsync(
            request,
            cancellationToken);

        if (resultado is null)
        {
            return Unauthorized(new
            {
                mensagem = "Telefone/E-mail ou senha inválidos."
            });
        }

        return Ok(resultado);
    }
}