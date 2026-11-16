
using MediatR;

namespace appServicosCostura.Application.TiposPecas.Commands;

public sealed record AlterarStatusTipoPecaCommand(
    Guid Id,
    bool Ativo
) : IRequest;
