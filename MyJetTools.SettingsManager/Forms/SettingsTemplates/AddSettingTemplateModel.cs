using MyJetTools.SettingsManager.Abstractions;

namespace MyJetTools.SettingsManager.Forms.SettingsTemplates;

public class AddSettingTemplateModel : ISettingsTemplate
{
    public string ServiceName { get; set;}
    public string SettingsYamlTemplate { get; set;}
    public string Env { get; set;}
    public DateTime CreateDate { get; set;}
    public DateTime LastUpdateDate { get; set;}

    public static AddSettingTemplateModel Create(ISettingsTemplate src)
    {
        return new AddSettingTemplateModel
        {
            ServiceName = src.ServiceName,
            SettingsYamlTemplate = src.SettingsYamlTemplate,
            Env = src.Env,
            CreateDate = src.CreateDate,
            LastUpdateDate = src.LastUpdateDate,
        };
    }
}