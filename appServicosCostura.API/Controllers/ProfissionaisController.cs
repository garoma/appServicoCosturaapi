using appServicosCostura.API.Helpers;
using appServicosCostura.API.Requests;
using appServicosCostura.Application.Profissionais.Commands;
using appServicosCostura.Application.Profissionais.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace appServicosCostura.API.Controllers;

[ApiController]
[Route("api/profissionais")]
public sealed class ProfissionaisController : ControllerBase
{
    private readonly ISender _sender;

    public ProfissionaisController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("criar")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Criar(
        [FromForm] CriarProfissionalRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var (foto, contentType) =
                await FotoUploadHelper.ConverterAsync(
                    request.Foto,
                    cancellationToken);

            var command = new CriarProfissionalCommand(
                request.Nome,
                request.Telefone,
                request.Email,
                request.Senha,
                request.Endereco,
                request.Bairro,
                request.Cidade,
                request.Cep,
                request.PontoReferencia,
                request.Servicos,
                request.Pecas,
                foto,
                contentType);

            var profissionalId = await _sender.Send(
                command,
                cancellationToken);

            return Created(
                $"/api/profissionais/{profissionalId}",
                new { id = profissionalId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> ListarTodos(
        CancellationToken cancellationToken)
    {
        var resultado = await _sender.Send(
            new ListarProfissionaisQuery(),
            cancellationToken);

        return Ok(resultado);
    }


    [HttpPut("perfil")]
    [Authorize(Roles = "Profissional")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AtualizarPerfil(
        [FromForm] AtualizarProfissionalRequest request,
        CancellationToken cancellationToken)
    {
        var idUsuario = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(idUsuario, out var profissionalId))
            return Unauthorized();

        try
        {
            var (foto, contentType) =
                await FotoUploadHelper.ConverterAsync(
                    request.Foto,
                    cancellationToken);

            var command = new AtualizarProfissionalCommand(
                profissionalId,
                request.Nome,
                request.Telefone,
                request.Email,
                request.Endereco,
                request.Bairro,
                request.Cidade,
                request.Cep,
                request.PontoReferencia,
                request.Servicos,
                request.Pecas,
                foto,
                contentType,
                request.RemoverFoto);

            await _sender.Send(command, cancellationToken);

            return Ok(new
            {
                mensagem = "Cadastro atualizado com sucesso."
            });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }


    [HttpGet("perfil")]
    [Authorize(Roles = "Profissional")]
    public async Task<IActionResult> ObterMeuPerfil(
        CancellationToken cancellationToken)
    {
        var idUsuario = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(idUsuario, out var profissionalId))
            return Unauthorized();

        var resultado = await _sender.Send(
            new ObterMeuPerfilProfissionalQuery(profissionalId),
            cancellationToken);

        if (resultado is null)
            return NotFound(new { erro = "Profissional não encontrado." });

        return Ok(resultado);
    }


}