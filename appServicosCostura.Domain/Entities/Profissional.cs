
namespace appServicosCostura.Domain.Entities;

public class Profissional
{
    public Guid Id { get; private set; }

    public string Nome { get; private set; } = string.Empty;

    public string Telefone { get; private set; } = string.Empty;

    public string SenhaHash { get; private set; } = string.Empty;

    public string? Email { get; private set; }

    public string Status { get; private set; } = "Ativo";

    public DateTime CriadoEmUtc { get; private set; }

    // Endereço
    public string Endereco { get; private set; } = string.Empty;

    public string Bairro { get; private set; } = string.Empty;

    public string Cidade { get; private set; } = string.Empty;

    public string Cep { get; private set; } = string.Empty;

    public string? PontoReferencia { get; private set; }

    // Dados profissionais
    public string Categoria { get; private set; } = string.Empty;

    public decimal PrecoBase { get; private set; }

    public List<string> Servicos { get; private set; } = [];

    public List<string> Pecas { get; private set; } = [];

    protected Profissional()
    {
    }

    public Profissional(
        string nome,
        string telefone,
        string categoria,
        decimal precoBase,
        string? email = null,
        string? endereco = null,
        string? bairro = null,
        string? cidade = null,
        string? cep = null,
        string? pontoReferencia = null,
        IEnumerable<string>? servicos = null,
        IEnumerable<string>? pecas = null)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(telefone))
            throw new ArgumentException("O telefone é obrigatório.");

        if (string.IsNullOrWhiteSpace(categoria))
            throw new ArgumentException("A categoria é obrigatória.");

        if (precoBase < 0)
            throw new ArgumentException("O preço base não pode ser negativo.");

        Id = Guid.NewGuid();
        Nome = nome.Trim();
        Telefone = telefone.Trim();
        Categoria = categoria.Trim();
        PrecoBase = precoBase;

        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();

        Endereco = endereco?.Trim() ?? string.Empty;
        Bairro = bairro?.Trim() ?? string.Empty;
        Cidade = cidade?.Trim() ?? string.Empty;
        Cep = cep?.Trim() ?? string.Empty;

        PontoReferencia = string.IsNullOrWhiteSpace(pontoReferencia)
            ? null
            : pontoReferencia.Trim();

        Servicos = servicos?.Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim()).Distinct().ToList() ?? [];

        Pecas = pecas?.Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim()).Distinct().ToList() ?? [];

        Status = "Ativo";
        CriadoEmUtc = DateTime.UtcNow;
    }

    public void DefinirSenhaHash(string senhaHash)
    {
        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new ArgumentException("O hash da senha é obrigatório.");

        SenhaHash = senhaHash;
    }
}
