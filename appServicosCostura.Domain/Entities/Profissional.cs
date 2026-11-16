
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

    public byte[]? Foto { get; private set; }

    public string? FotoContentType { get; private set; }

    // Dados profissionais
    public List<string> Servicos { get; private set; } = [];

    public List<string> Pecas { get; private set; } = [];

    protected Profissional()
    {
    }

    public Profissional(
        string nome,
        string telefone,
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

        Id = Guid.NewGuid();
        Nome = nome.Trim();
        Telefone = telefone.Trim();

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


    public void AtualizarPerfil(
        string nome,
        string telefone,
        string? email,
        string? endereco,
        string? bairro,
        string? cidade,
        string? cep,
        string? pontoReferencia,
        IEnumerable<string>? servicos,
        IEnumerable<string>? pecas)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(telefone))
            throw new ArgumentException("O telefone é obrigatório.");

        Nome = nome.Trim();
        Telefone = telefone.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim();

        Endereco = endereco?.Trim() ?? string.Empty;
        Bairro = bairro?.Trim() ?? string.Empty;
        Cidade = cidade?.Trim() ?? string.Empty;
        Cep = cep?.Trim() ?? string.Empty;

        PontoReferencia = string.IsNullOrWhiteSpace(pontoReferencia)
            ? null
            : pontoReferencia.Trim();

        Servicos = servicos?
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct()
            .ToList() ?? [];

        Pecas = pecas?
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => x.Trim())
            .Distinct()
            .ToList() ?? [];
    }
    
    public void DefinirSenhaHash(string senhaHash)
    {
        if (string.IsNullOrWhiteSpace(senhaHash))
            throw new ArgumentException("O hash da senha é obrigatório.");

        SenhaHash = senhaHash;
    }

    public void DefinirFoto(byte[]? foto, string? contentType)
    {
        Foto = foto;
        FotoContentType = foto is null ? null : contentType;
    }
}
