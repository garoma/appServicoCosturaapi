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
    public async Task<IActionResult> Criar(
        [FromBody] CriarClienteCommand command,
        CancellationToken cancellationToken)
    {
        var clienteId = await _sender.Send(
            command,
            cancellationToken);

        return Created(
            $"/api/clientes/{clienteId}",
            new { id = clienteId });
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
}
