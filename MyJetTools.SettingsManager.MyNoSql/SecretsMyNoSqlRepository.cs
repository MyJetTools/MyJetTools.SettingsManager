using MyJetTools.SettingsManager.Abstractions;
using MyNoSqlServer.Abstractions;

namespace MyJetTools.SettingsManager.MyNoSql;

public class SecretsMyNoSqlRepository : ISecretRepository
{
    private readonly IMyNoSqlServerDataWriter<SecretMyNoSqlModel> _secretsWriter;
    private readonly ISecretValuesStore _secretValuesStore;

    public SecretsMyNoSqlRepository(IMyNoSqlServerDataWriter<SecretMyNoSqlModel> secretsWriter,
        ISecretValuesStore secretValuesStore)
    {
        _secretsWriter = secretsWriter;
        _secretValuesStore = secretValuesStore;
    }

    public async Task<IEnumerable<ISecretModel>> GetSecretsAsync()
    {
        var secrets = await _secretsWriter.GetAsync();

        var result = new List<ISecretModel>();
        foreach (var secret in secrets ?? Array.Empty<SecretMyNoSqlModel>())
        {
            try
            {
                secret.Value = await _secretValuesStore.GetSecretValueAsync(secret.Name);
            }
            catch (Exception e)
            {
                secret.Value = "Not found in key vault";
            }
            
            result.Add(secret);
        }

        return result;
    }

    public async Task AddSecret(ISecretModel secret)
    {
        await _secretValuesStore.SaveSecretValueAsync(secret.Name, secret.Value);
        var modelToInsert = SecretMyNoSqlModel.Create(secret);
        modelToInsert.CreateDate = DateTime.UtcNow;
        modelToInsert.LastUpdate = DateTime.UtcNow;
        await _secretsWriter.InsertAsync(modelToInsert);
    }

    public async Task UpdateSecret(ISecretModel secret)
    {
        await _secretValuesStore.UpdateSecretValue(secret.Name, secret.Value);
        var modelToInsert = SecretMyNoSqlModel.Create(secret);
        modelToInsert.LastUpdate = DateTime.UtcNow;
        await _secretsWriter.InsertOrReplaceAsync(modelToInsert);
    }

    public async Task RemoveSecret(ISecretModel secret)
    {
        await _secretValuesStore.RemoveSecret(secret.Name);
        var pk = SecretMyNoSqlModel.GeneratePartitionKey();
        var rk = SecretMyNoSqlModel.GenerateRowKey(secret.Name);
        await _secretsWriter.DeleteAsync(pk, rk);
    }
}