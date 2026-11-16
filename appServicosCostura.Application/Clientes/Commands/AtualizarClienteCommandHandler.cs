
using appServicosCostura.Application.Interfaces;
using MediatR;

namespace appServicosCostura.Application.Clientes.Commands;

public sealed class AtualizarClienteCommandHandler
    : IRequestHandler<AtualizarClienteCommand, bool>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IProfissionalRepository _profissionalRepository;

    public AtualizarClienteCommandHandler(
        IClienteRepository clienteRepository,
        IProfissionalRepository profissionalRepository)
    {
        _clienteRepository = clienteRepository;
        _profissionalRepository = profissionalRepository;
    }

    public async Task<bool> Handle(
        AtualizarClienteCommand request,
        CancellationToken cancellationToken)
    {
        var cliente = await _clienteRepository.ObterPorIdAsync(
            request.ClienteId,
            cancellationToken);

        if (cliente is null)
            throw new ArgumentException("Cliente não encontrado.");

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

        var telefone = request.Telefone.Trim();

        if (await _clienteRepository.TelefoneExisteParaOutroClienteAsync(
            telefone, cliente.Id, cancellationToken))
        {
            throw new ArgumentException(
                "Este telefone já está cadastrado.");
        }

        var profissionalTelefone =
            await _profissionalRepository.ObterPorTelefoneAsync(
                telefone, cancellationToken);

        if (profissionalTelefone is not null)
            throw new ArgumentException(
                "Este telefone já está cadastrado.");

        var email = string.IsNullOrWhiteSpace(request.Email)
            ? null
            : request.Email.Trim();

        if (email is not null)
        {
            if (await _clienteRepository.EmailExisteParaOutroClienteAsync(
                email, cliente.Id, cancellationToken))
            {
                throw new ArgumentException(
                    "Este e-mail já está cadastrado.");
            }

            var profissionalEmail =
                await _profissionalRepository.ObterPorEmailAsync(
                    email, cancellationToken);

            if (profissionalEmail is not null)
                throw new ArgumentException(
                    "Este e-mail já está cadastrado.");
        }

        cliente.AtualizarPerfil(
            request.Nome,
            telefone,
            email,
            request.Endereco,
            request.Bairro,
            request.Cidade,
            request.Cep,
            request.PontoReferencia);

        if (request.RemoverFoto)
        {
            cliente.DefinirFoto(null, null);
        }
        else if (request.Foto is not null)
        {
            cliente.DefinirFoto(
                request.Foto,
                request.FotoContentType);
        }

        await _clienteRepository.SalvarAlteracoesAsync(
            cancellationToken);

        return true;
    }
}
