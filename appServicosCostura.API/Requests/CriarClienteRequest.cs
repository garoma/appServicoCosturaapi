using Microsoft.AspNetCore.Http;

namespace appServicosCostura.API.Requests;

public sealed class CriarClienteRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Senha { get; set; } = string.Empty;

    public string? Endereco { get; set; }
    public string? Bairro { get; set; }
    public string? Cidade { get; set; }
    public string? Cep { get; set; }
    public string? PontoReferencia { get; set; }

    public IFormFile? Foto { get; set; }
}