
namespace appServicosCostura.Domain.Entities;

public class TipoPeca
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public bool Ativo { get; private set; }
    public DateTime CriadoEmUtc { get; private set; }

    protected TipoPeca() { }

    public TipoPeca(string nome)
    {
        DefinirNome(nome);

        Id = Guid.NewGuid();
        Ativo = true;
        CriadoEmUtc = DateTime.UtcNow;
    }

    public void Atualizar(string nome)
    {
        DefinirNome(nome);
    }

    public void AlterarStatus(bool ativo)
    {
        Ativo = ativo;
    }

    private void DefinirNome(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("O nome da peça é obrigatório.");

        Nome = nome.Trim();
    }
}
