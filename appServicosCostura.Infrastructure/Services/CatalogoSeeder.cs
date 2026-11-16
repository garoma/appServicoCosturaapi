
using appServicosCostura.Domain.Entities;
using appServicosCostura.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace appServicosCostura.Infrastructure.Services;

public sealed class CatalogoSeeder(
    appServicosCosturaDbContext context)
{
    public async Task SeedAsync(
        CancellationToken cancellationToken = default)
    {
        //dados oficiais (para quando for para producao)
        //var servicosIniciais = new[]
        //{
        //    "Modelagem/Risco",
        //    "Corte",
        //    "Traseira",
        //    "Cós",
        //    "Cabecinha",
        //    "Travete",
        //    "Caseado"
        //};

        //var pecasIniciais = new[]
        //{
        //    "Calça",
        //    "Short",
        //    "Bermuda",
        //    "Saia",
        //    "Jaqueta",
        //    "Outros"
        //};

        //dados para desenvolvimento (teste) 
        var servicosIniciais = new[]
{
            "Traseira",
            "Frente",
            "Cós",
            "Cós",
            "Travete",
            "Cabecinha"
        };

        var pecasIniciais = new[]
        {
            "Calça",
            "Short",
            "Bermuda",
            "Saia",
            "Vestido",
            "Camisa",
            "Jaqueta"
        };

        var nomesServicosExistentes = (
            await context.TiposServicos
                .Select(x => x.Nome)
                .ToListAsync(cancellationToken))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        var nomesPecasExistentes = (
            await context.TiposPecas
                .Select(x => x.Nome)
                .ToListAsync(cancellationToken))
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var nome in servicosIniciais)
        {
            if (nomesServicosExistentes.Add(nome))
                context.TiposServicos.Add(new TipoServico(nome));
        }

        foreach (var nome in pecasIniciais)
        {
            if (nomesPecasExistentes.Add(nome))
                context.TiposPecas.Add(new TipoPeca(nome));
        }

        await context.SaveChangesAsync(cancellationToken);
    }
}
