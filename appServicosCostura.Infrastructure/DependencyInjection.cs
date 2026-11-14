
using appServicosCostura.Application.Autenticacao;
using appServicosCostura.Application.Interfaces;
using appServicosCostura.Domain.Entities;
using appServicosCostura.Infrastructure.Persistence;
using appServicosCostura.Infrastructure.Repositories;
using appServicosCostura.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace appServicosCostura.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString =
            configuration.GetConnectionString("appServicosCostura")
            ?? throw new InvalidOperationException(
                "A conexão appServicosCostura não foi configurada.");

        services.AddDbContext<appServicosCosturaDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IAdminUserRepository, AdminUserRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordHasher<AdminUser>, PasswordHasher<AdminUser>>();
        services.AddScoped<IPasswordHasher<Cliente>, PasswordHasher<Cliente>>();
        services.AddScoped<IPasswordHasher<Profissional>, PasswordHasher<Profissional>>();
        services.AddScoped<ILoginAdminService, LoginAdminService>();
        services.AddScoped<ILoginClienteService, LoginClienteService>();
        services.AddScoped<ILoginProfissionalService, LoginProfissionalService>();
        services.AddScoped<AdminSeeder>();
        services.AddScoped<IClienteRepository, ClienteRepository>();
        services.AddScoped<IProfissionalRepository, ProfissionalRepository>();
        return services;
    }
}
