using Flurl.Http;
using YamlDotNet.Serialization;

namespace MyJetTools.SettingsManager.Client;

public class SettingsReader
{
    public static T GetSettings<T>(string settings = "./settings")
    {
        var (settingsString, path) = GetSettingsAsString(settings);
        
        if (string.IsNullOrEmpty(settingsString))
            throw new Exception($"Setting not found. Path: {path}");

        var deserializer = new DeserializerBuilder()
            .IgnoreUnmatchedProperties()
            .Build();

        return deserializer.Deserialize<T>(settingsString);
    }

    private static (string, string) GetSettingsAsString(string settingsPath)
    {
        var envVariable = Environment.GetEnvironmentVariable("SETTINGS_URL");

        if (string.IsNullOrEmpty(envVariable)) 
            return (File.ReadAllText(settingsPath), settingsPath);
        
        var settingsResponse = envVariable
            .WithClient(new FlurlClient(new HttpClient(new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
            })))
            .GetStringAsync().Result;

        return (settingsResponse, envVariable);
    } 
}