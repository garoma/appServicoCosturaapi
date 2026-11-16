
using MediatR;

namespace appServicosCostura.Application.TiposServicos.Commands;

public sealed record AlterarStatusTipoServicoCommand(
    Guid Id,
    bool Ativo
) : IRequest;
