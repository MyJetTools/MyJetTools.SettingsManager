using Azure.Security.KeyVault.Secrets;
using MyJetTools.SettingsManager.Abstractions;

namespace MyJetTools.SettingsManager.Services;

public class AzureKeyValuesSecretsStore : ISecretValuesStore
{
    private readonly SecretClient _secretClient;

    private readonly Dictionary<string, string> _secretsCache = new ();

    public AzureKeyValuesSecretsStore(SecretClient secretClient)
    {
        _secretClient = secretClient;
    }

    public async Task<string> GetSecretValueAsync(string name)
    {
        if (_secretsCache.ContainsKey(name))
        {
            return _secretsCache[name];
        }
        
        try
        {
            var result = await _secretClient.GetSecretAsync(name);
            var value = result.Value.Value;

            _secretsCache[name] = value;
            return value;
        }
        catch (Exception)
        {
            return string.Empty;
        }
        
    }

    public async Task SaveSecretValueAsync(string name, string value)
    {
        await _secretClient.SetSecretAsync(name, value);
        _secretsCache.Clear();
    }

    public async Task UpdateSecretValue(string name, string value)
    {
        await _secretClient.SetSecretAsync(name, value);
        _secretsCache.Clear();
    }

    public async Task RemoveSecret(string name)
    {
        try
        {
            await _secretClient.StartDeleteSecretAsync(name);
            _secretsCache.Clear();
        }
        catch
        {
            // ignored
        }
    }
}