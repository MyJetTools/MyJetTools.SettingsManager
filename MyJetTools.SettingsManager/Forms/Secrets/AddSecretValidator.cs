using FluentValidation;

namespace MyJetTools.SettingsManager.Forms.Secrets;

public class AddSecretValidator : AbstractValidator<AddSecretFormModel>
{
    public AddSecretValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .Length(1, 100);

        RuleFor(x => x.Value)
            .NotEmpty()
            .Length(1, 1000);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result =
            await ValidateAsync(
                ValidationContext<AddSecretFormModel>.CreateWithOptions((AddSecretFormModel) model,
                    x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}