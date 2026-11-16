
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.Profissionais.Commands;

public sealed class AtualizarProfissionalCommandHandler
    : IRequestHandler<AtualizarProfissionalCommand, bool>
{
    private readonly IProfissionalRepository _profissionalRepository;
    private readonly IClienteRepository _clienteRepository;

    public AtualizarProfissionalCommandHandler(
        IProfissionalRepository profissionalRepository,
        IClienteRepository clienteRepository)
    {
        _profissionalRepository = profissionalRepository;
        _clienteRepository = clienteRepository;
    }

    public async Task<bool> Handle(
        AtualizarProfissionalCommand request,
        CancellationToken cancellationToken)
    {
        var profissional =
            await _profissionalRepository.ObterPorIdAsync(
                request.ProfissionalId,
                cancellationToken);

        if (profissional is null)
            throw new ArgumentException("Profissional não encontrado.");

        if (string.IsNullOrWhiteSpace(request.Nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Telefone))
            throw new ArgumentException("O telefone é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Endereco))
            throw new ArgumentException("O endereço é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Bairro))
            throw new ArgumentException("O bairro é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Cidade))
            throw new ArgumentException("A cidade é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Cep))
            throw new ArgumentException("O CEP é obrigatório.");

        if (request.Servicos is null || request.Servicos.Count == 0)
            throw new ArgumentException(
                "Informe pelo menos um serviço realizado.");

        if (request.Pecas is null || request.Pecas.Count == 0)
            throw new ArgumentException(
                "Informe pelo menos um tipo de peça atendida.");

        var telefone = request.Telefone.Trim();

        if (await _profissionalRepository
            .TelefoneExisteParaOutroProfissionalAsync(
                telefone, profissional.Id, cancellationToken))
        {
            throw new ArgumentException(
                "Este telefone já está cadastrado.");
        }

        var clienteTelefone =
            await _clienteRepository.ObterPorTelefoneAsync(
                telefone, cancellationToken);

        if (clienteTelefone is not null)
            throw new ArgumentException(
                "Este telefone já está cadastrado.");

        var email = string.IsNullOrWhiteSpace(request.Email)
            ? null
            : request.Email.Trim();

        if (email is not null)
        {
            if (await _profissionalRepository
                .EmailExisteParaOutroProfissionalAsync(
                    email, profissional.Id, cancellationToken))
            {
                throw new ArgumentException(
                    "Este e-mail já está cadastrado.");
            }

            var clienteEmail =
                await _clienteRepository.ObterPorEmailAsync(
                    email, cancellationToken);

            if (clienteEmail is not null)
                throw new ArgumentException(
                    "Este e-mail já está cadastrado.");
        }

        profissional.AtualizarPerfil(
            request.Nome,
            telefone,
            email,
            request.Endereco,
            request.Bairro,
            request.Cidade,
            request.Cep,
            request.PontoReferencia,
            request.Servicos,
            request.Pecas);

        if (request.RemoverFoto)
        {
            profissional.DefinirFoto(null, null);
        }
        else if (request.Foto is not null)
        {
            profissional.DefinirFoto(
                request.Foto,
                request.FotoContentType);
        }

        await _profissionalRepository.SalvarAlteracoesAsync(
            cancellationToken);

        return true;
    }
}
