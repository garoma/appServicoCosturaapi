
using MediatR;

namespace appServicosCostura.Application.TiposPecas.Commands;

public sealed record CriarTipoPecaCommand(
    string Nome
) : IRequest<Guid>;
