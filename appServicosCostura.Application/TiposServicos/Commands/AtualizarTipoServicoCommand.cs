
using MediatR;

namespace appServicosCostura.Application.TiposServicos.Commands;

public sealed record AtualizarTipoServicoCommand(
    Guid Id,
    string Nome
) : IRequest;
