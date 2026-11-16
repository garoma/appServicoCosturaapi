
using appServicosCostura.Application.TiposPecas.Commands;
using appServicosCostura.Application.TiposPecas.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace appServicosCostura.API.Controllers;

[ApiController]
[Route("api/tipos-pecas")]
public sealed class TiposPecasController : ControllerBase
{
    private readonly ISender _sender;

    public TiposPecasController(ISender sender)
    {
        _sender = sender;
    }

    // Consulta pública: somente peças ativas
    [HttpGet("ativos")]
    [AllowAnonymous]
    public async Task<IActionResult> ListarAtivos(
        CancellationToken cancellationToken)
    {
        var resultado = await _sender.Send(
            new ListarTiposPecasAtivosQuery(),
            cancellationToken);

        return Ok(resultado);
    }

    // Consulta administrativa: todas as peças
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ListarTodos(
        CancellationToken cancellationToken)
    {
        var resultado = await _sender.Send(
            new ListarTiposPecasQuery(),
            cancellationToken);

        return Ok(resultado);
    }

    // Cadastrar
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Criar(
        [FromBody] CriarTipoPecaCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var id = await _sender.Send(request, cancellationToken);

            return Created(
                $"/api/tipos-pecas/{id}",
                new { id });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
    }

    // Editar
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Atualizar(
        Guid id,
        [FromBody] AtualizarTipoPecaCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _sender.Send(
                request with { Id = id },
                cancellationToken);

            return Ok(new { mensagem = "Tipo de peça atualizado com sucesso." });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
    }

    // Ativar/desativar
    [HttpPatch("{id:guid}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AlterarStatus(
        Guid id,
        [FromBody] AlterarStatusTipoPecaCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _sender.Send(
                request with { Id = id },
                cancellationToken);

            return Ok(new { mensagem = "Status da peça atualizado com sucesso." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
    }
}
