
using appServicosCostura.Application.Clientes.Commands;
using appServicosCostura.Application.Interfaces;
using appServicosCostura.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace appServicosCostura.Application.Profissionais.Commands;

public sealed class CriarProfissionalCommandHandler
    : IRequestHandler<CriarProfissionalCommand, Guid>
{
    private readonly IProfissionalRepository _profissionalRepository;
    private readonly IClienteRepository _clienteRepository;
    private readonly IPasswordHasher<Profissional> _passwordHasher;

    public CriarProfissionalCommandHandler(
        IProfissionalRepository profissionalRepository,
        IClienteRepository clienteRepository,
        IPasswordHasher<Profissional> passwordHasher)
    {
        _profissionalRepository = profissionalRepository;
        _clienteRepository = clienteRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(
        CriarProfissionalCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Senha) ||
            request.Senha.Length < 8)
        {
            throw new ArgumentException(
                "A senha deve possuir pelo menos 8 caracteres.");
        }

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

        var profissionalExistente =
            await _profissionalRepository.ObterPorTelefoneAsync(
                request.Telefone, cancellationToken);

        var clienteExistente =
            await _clienteRepository.ObterPorTelefoneAsync(
                request.Telefone, cancellationToken);

        if (profissionalExistente is not null ||
            clienteExistente is not null)
        {
            throw new ArgumentException(
                "Este telefone já está cadastrado.");
        }

        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var profissionalEmail =
                await _profissionalRepository.ObterPorEmailAsync(
                    request.Email, cancellationToken);

            var clienteEmail =
                await _clienteRepository.ObterPorEmailAsync(
                    request.Email, cancellationToken);

            if (profissionalEmail is not null ||
                clienteEmail is not null)
            {
                throw new ArgumentException(
                    "Este e-mail já está cadastrado.");
            }
        }

        var profissional = new Profissional(
            request.Nome,
            request.Telefone,
            request.Email,
            request.Endereco,
            request.Bairro,
            request.Cidade,
            request.Cep,
            request.PontoReferencia,
            request.Servicos,
            request.Pecas);

        if (request.Foto is not null)
        {
            profissional.DefinirFoto(
                request.Foto,
                request.FotoContentType);
        }

        var senhaHash = _passwordHasher.HashPassword(
            profissional,
            request.Senha);

        profissional.DefinirSenhaHash(senhaHash);

        await _profissionalRepository.AdicionarAsync(
            profissional, cancellationToken);

        await _profissionalRepository.SalvarAlteracoesAsync(
            cancellationToken);

        return profissional.Id;
    }
}
