namespace MyJetTools.SettingsManager.Abstractions;

public interface ISettingsTemplate
{ 
    string ServiceName { get; }
    string SettingsYamlTemplate { get; }
    string Env { get; }
    DateTime CreateDate { get; }
    DateTime LastUpdateDate { get; }
}

public class SettingsTemplateModel : ISettingsTemplate
{
    public string ServiceName { get; set;}
    public string SettingsYamlTemplate { get; set;}
    public string Env { get; set;}
    public DateTime CreateDate { get; set;}
    public DateTime LastUpdateDate { get; set;}

    public static SettingsTemplateModel Create(ISettingsTemplate src)
    {
        return new SettingsTemplateModel
        {
            ServiceName = src.ServiceName,
            SettingsYamlTemplate = src.SettingsYamlTemplate,
            Env = src.Env,
            CreateDate = src.CreateDate,
            LastUpdateDate = src.LastUpdateDate,
        };
    }
}