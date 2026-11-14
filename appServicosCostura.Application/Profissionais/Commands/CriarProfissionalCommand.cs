
using MediatR;

namespace appServicosCostura.Application.Profissionais.Commands;

public sealed record CriarProfissionalCommand(
    string Nome,
    string Telefone,
    string? Email,
    string Senha,
    string Endereco,
    string Bairro,
    string Cidade,
    string Cep,
    string? PontoReferencia,
    string Categoria,
    decimal PrecoBase,
    List<string> Servicos,
    List<string> Pecas
) : IRequest<Guid>;
