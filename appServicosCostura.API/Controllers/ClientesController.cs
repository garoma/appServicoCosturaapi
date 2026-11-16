using appServicosCostura.API.Helpers;
using appServicosCostura.API.Requests;
using appServicosCostura.Application.Clientes.Commands;
using appServicosCostura.Application.Clientes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace appServicosCostura.API.Controllers;

[ApiController]
[Route("api/clientes")]
public sealed class ClientesController : ControllerBase
{
    private readonly ISender _sender;

    public ClientesController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("criar")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Criar(
        [FromForm] CriarClienteRequest request,
        CancellationToken cancellationToken)
    {
        try
        {
            var (foto, contentType) =
                await FotoUploadHelper.ConverterAsync(
                    request.Foto,
                    cancellationToken);

            var command = new CriarClienteCommand(
                request.Nome,
                request.Telefone,
                request.Email,
                request.Senha,
                request.Endereco,
                request.Bairro,
                request.Cidade,
                request.Cep,
                request.PontoReferencia,
                foto,
                contentType);

            var clienteId = await _sender.Send(
                command,
                cancellationToken);

            return Created(
                $"/api/clientes/{clienteId}",
                new { id = clienteId });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ListarTodos(
        CancellationToken cancellationToken)
    {
        var resultado = await _sender.Send(
            new ListarClientesQuery(),
            cancellationToken);

        return Ok(resultado);
    }


    [HttpPut("perfil")]
    [Authorize(Roles = "Cliente")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> AtualizarPerfil(
        [FromForm] AtualizarClienteRequest request,
        CancellationToken cancellationToken)
    {
        var idUsuario = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(idUsuario, out var clienteId))
            return Unauthorized();

        try
        {
            var (foto, contentType) =
                await FotoUploadHelper.ConverterAsync(
                    request.Foto,
                    cancellationToken);

            var command = new AtualizarClienteCommand(
                clienteId,
                request.Nome,
                request.Telefone,
                request.Email,
                request.Endereco,
                request.Bairro,
                request.Cidade,
                request.Cep,
                request.PontoReferencia,
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
    [Authorize(Roles = "Cliente")]
    public async Task<IActionResult> ObterMeuPerfil(
        CancellationToken cancellationToken)
    {
        var idUsuario = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(idUsuario, out var clienteId))
            return Unauthorized();

        var resultado = await _sender.Send(
            new ObterMeuPerfilClienteQuery(clienteId),
            cancellationToken);

        if (resultado is null)
            return NotFound(new { erro = "Cliente não encontrado." });

        return Ok(resultado);
    }


}