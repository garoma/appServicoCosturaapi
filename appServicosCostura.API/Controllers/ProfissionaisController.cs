
using appServicosCostura.Application.Profissionais.Commands;
using appServicosCostura.Application.Profissionais.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<IActionResult> Criar(
        [FromBody] CriarProfissionalCommand command,
        CancellationToken cancellationToken)
    {
        var profissionalId = await _sender.Send(
            command,
            cancellationToken);

        return Created(
            $"/api/profissionais/{profissionalId}",
            new { id = profissionalId });
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
}
