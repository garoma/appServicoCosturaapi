
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.Profissionais.Queries;

public sealed record ObterMeuPerfilProfissionalQuery(Guid ProfissionalId)
    : IRequest<ObterMeuPerfilProfissionalResult?>;

public sealed record ObterMeuPerfilProfissionalResult(
    Guid Id,
    string Nome,
    string Telefone,
    string? Email,
    string Endereco,
    string Bairro,
    string Cidade,
    string Cep,
    string? PontoReferencia,
    string? Foto,
    string? FotoContentType,
    List<string> Servicos,
    List<string> Pecas
);

public sealed class ObterMeuPerfilProfissionalQueryHandler
    : IRequestHandler<
        ObterMeuPerfilProfissionalQuery,
        ObterMeuPerfilProfissionalResult?>
{
    private readonly IProfissionalRepository _repository;

    public ObterMeuPerfilProfissionalQueryHandler(
        IProfissionalRepository repository)
    {
        _repository = repository;
    }

    public async Task<ObterMeuPerfilProfissionalResult?> Handle(
        ObterMeuPerfilProfissionalQuery request,
        CancellationToken cancellationToken)
    {
        var profissional = await _repository.ObterPorIdAsync(
            request.ProfissionalId,
            cancellationToken);

        if (profissional is null)
            return null;

        var foto = profissional.Foto is { Length: > 0 }
            ? Convert.ToBase64String(profissional.Foto)
            : null;

        return new ObterMeuPerfilProfissionalResult(
            profissional.Id,
            profissional.Nome,
            profissional.Telefone,
            profissional.Email,
            profissional.Endereco,
            profissional.Bairro,
            profissional.Cidade,
            profissional.Cep,
            profissional.PontoReferencia,
            foto,
            profissional.FotoContentType,
            profissional.Servicos,
            profissional.Pecas
        );
    }
}
