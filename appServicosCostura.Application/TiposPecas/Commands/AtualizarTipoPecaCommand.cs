
using MediatR;

namespace appServicosCostura.Application.TiposPecas.Commands;

public sealed record AtualizarTipoPecaCommand(
    Guid Id,
    string Nome
) : IRequest;
