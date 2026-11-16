
using appServicosCostura.Application.Interfaces;
using appServicosCostura.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace appServicosCostura.Application.Clientes.Commands;

public sealed class CriarClienteCommandHandler
    : IRequestHandler<CriarClienteCommand, Guid>
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IPasswordHasher<Cliente> _passwordHasher;

    public CriarClienteCommandHandler(
        IClienteRepository clienteRepository,
        IPasswordHasher<Cliente> passwordHasher)
    {
        _clienteRepository = clienteRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<Guid> Handle(
        CriarClienteCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Senha) ||
            request.Senha.Length < 8)
        {
            throw new ArgumentException(
                "A senha deve possuir pelo menos 8 caracteres.");
        }

        if (string.IsNullOrWhiteSpace(request.Endereco))
            throw new ArgumentException("O endereço é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Bairro))
            throw new ArgumentException("O bairro é obrigatório.");

        if (string.IsNullOrWhiteSpace(request.Cidade))
            throw new ArgumentException("A cidade é obrigatória.");

        if (string.IsNullOrWhiteSpace(request.Cep))
            throw new ArgumentException("O CEP é obrigatório.");

        var cliente = new Cliente(
            request.Nome,
            request.Telefone,
            request.Email,
            request.Endereco,
            request.Bairro,
            request.Cidade,
            request.Cep,
            request.PontoReferencia);

        if (request.Foto is not null)
        {
            cliente.DefinirFoto(
                request.Foto,
                request.FotoContentType);
        }

        var senhaHash = _passwordHasher.HashPassword(
            cliente,
            request.Senha);

        cliente.DefinirSenhaHash(senhaHash);

        await _clienteRepository.AdicionarAsync(
            cliente,
            cancellationToken);

        await _clienteRepository.SalvarAlteracoesAsync(
            cancellationToken);

        return cliente.Id;
    }
}
