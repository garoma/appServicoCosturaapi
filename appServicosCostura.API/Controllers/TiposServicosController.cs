
using appServicosCostura.Application.TiposServicos.Commands;
using appServicosCostura.Application.TiposServicos.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace appServicosCostura.API.Controllers;

[ApiController]
[Route("api/tipos-servicos")]
public sealed class TiposServicosController : ControllerBase
{
    private readonly ISender _sender;

    public TiposServicosController(ISender sender)
    {
        _sender = sender;
    }

    // Consulta pública: somente serviços ativos
    [HttpGet("ativos")]
    [AllowAnonymous]
    public async Task<IActionResult> ListarAtivos(
        CancellationToken cancellationToken)
    {
        var resultado = await _sender.Send(
            new ListarTiposServicosAtivosQuery(),
            cancellationToken);

        return Ok(resultado);
    }

    // Consulta administrativa: todos os serviços
    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ListarTodos(
        CancellationToken cancellationToken)
    {
        var resultado = await _sender.Send(
            new ListarTiposServicosQuery(),
            cancellationToken);

        return Ok(resultado);
    }

    // Cadastrar
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Criar(
        [FromBody] CriarTipoServicoCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var id = await _sender.Send(request, cancellationToken);

            return Created(
                $"/api/tipos-servicos/{id}",
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
        [FromBody] AtualizarTipoServicoCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _sender.Send(
                request with { Id = id },
                cancellationToken);

            return Ok(new { mensagem = "Tipo de serviço atualizado com sucesso." });
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
        [FromBody] AlterarStatusTipoServicoCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            await _sender.Send(
                request with { Id = id },
                cancellationToken);

            return Ok(new { mensagem = "Status do serviço atualizado com sucesso." });
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { erro = ex.Message });
        }
    }
}
