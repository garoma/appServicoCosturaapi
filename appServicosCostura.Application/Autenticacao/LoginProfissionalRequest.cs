using FluentValidation;

namespace appServicosCostura.Application.Autenticacao;

public sealed record LoginProfissionalRequest(
    string Identificador,
    string Senha);

public sealed class LoginProfissionalValidator
    : AbstractValidator<LoginProfissionalRequest>
{
    public LoginProfissionalValidator()
    {
        RuleFor(x => x.Identificador)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Senha)
            .NotEmpty()
            .MaximumLength(200);
    }
}