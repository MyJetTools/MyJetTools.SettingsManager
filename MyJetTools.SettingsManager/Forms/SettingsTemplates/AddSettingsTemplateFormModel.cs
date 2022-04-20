using FluentValidation;

namespace MyJetTools.SettingsManager.Forms.SettingsTemplates;

public class AddSettingsTemplateFormModel : AbstractValidator<AddSettingTemplateModel>
{
    public AddSettingsTemplateFormModel()
    {
        RuleFor(itm => itm.Env)
            .NotEmpty()
            .Length(1, 1000);

        RuleFor(itm => itm.ServiceName)
            .NotEmpty()
            .Length(1, 1000);
    }

    public Func<object, string, Task<IEnumerable<string>>> ValidateValue => async (model, propertyName) =>
    {
        var result =
            await ValidateAsync(
                ValidationContext<AddSettingTemplateModel>.CreateWithOptions((AddSettingTemplateModel) model,
                    x => x.IncludeProperties(propertyName)));
        return result.IsValid ? Array.Empty<string>() : result.Errors.Select(e => e.ErrorMessage);
    };
}