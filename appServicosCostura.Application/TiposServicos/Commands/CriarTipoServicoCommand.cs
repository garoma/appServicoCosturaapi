
using MediatR;

namespace appServicosCostura.Application.TiposServicos.Commands;

public sealed record CriarTipoServicoCommand(
    string Nome
) : IRequest<Guid>;
