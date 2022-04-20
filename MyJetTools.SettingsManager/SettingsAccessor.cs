using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace MyJetTools.SettingsManager;

public class SettingsModel
{
    public string KeyVaultUrl { get; set; }
    public string MyNoSqlWriterUrl { get; set; }
}
public class SettingsAccessor
{
    public SettingsModel GetSettings()
    {
        var settingsUrl = Environment.GetEnvironmentVariable("SETTINGS_PATH");

        if (string.IsNullOrEmpty(settingsUrl))
        {
            throw new Exception("Path to settings not found. Need SETTINGS_PATH env variable");
        }

        var file = File.ReadAllText(settingsUrl);
        
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(UnderscoredNamingConvention.Instance)  // see height_in_inches in sample yml 
            .Build();

        return deserializer.Deserialize<SettingsModel>(file);
    }
}