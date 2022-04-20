namespace MyJetTools.SettingsManager.Abstractions;

public interface ISecretRepository
{
    Task<IEnumerable<ISecretModel>> GetSecretsAsync();
    Task AddSecret(ISecretModel secret);
    Task UpdateSecret(ISecretModel secret);
    Task RemoveSecret(ISecretModel secret);
}