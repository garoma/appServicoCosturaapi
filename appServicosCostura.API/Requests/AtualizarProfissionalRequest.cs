
using Microsoft.AspNetCore.Http;

namespace appServicosCostura.API.Requests;

public sealed class AtualizarProfissionalRequest
{
    public string Nome { get; set; } = string.Empty;
    public string Telefone { get; set; } = string.Empty;
    public string? Email { get; set; }
    public string Endereco { get; set; } = string.Empty;
    public string Bairro { get; set; } = string.Empty;
    public string Cidade { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public string? PontoReferencia { get; set; }

    public List<string> Servicos { get; set; } = [];
    public List<string> Pecas { get; set; } = [];

    public IFormFile? Foto { get; set; }

    public bool RemoverFoto { get; set; }
}
