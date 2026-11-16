
using appServicosCostura.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace appServicosCostura.Infrastructure.Persistence.Configurations;

public sealed class TipoPecaConfiguration
    : IEntityTypeConfiguration<TipoPeca>
{
    public void Configure(EntityTypeBuilder<TipoPeca> builder)
    {
        builder.ToTable("tipospecas");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();

        builder.Property(t => t.Nome)
            .HasColumnName("nome")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.Ativo)
            .HasColumnName("ativo")
            .IsRequired();

        builder.Property(t => t.CriadoEmUtc)
            .HasColumnName("criado_em_utc")
            .IsRequired();

        builder.HasIndex(t => t.Nome)
            .IsUnique();
    }
}
