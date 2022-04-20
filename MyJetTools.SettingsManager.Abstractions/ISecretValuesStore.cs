namespace MyJetTools.SettingsManager.Abstractions;

public interface ISecretValuesStore
{
    Task<string> GetSecretValueAsync(string name);
    Task SaveSecretValueAsync(string name, string value);
    Task UpdateSecretValue(string name, string value);
    Task RemoveSecret(string name);
}