using MyJetTools.SettingsManager.Abstractions;
using MyNoSqlServer.Abstractions;

namespace MyJetTools.SettingsManager.MyNoSql;

public class SettingsTemplateMyNoSqlRepository : ISettingsTemplateRepository
{
    private readonly IMyNoSqlServerDataWriter<SettingTemplateMyNoSqlModel> _dataWriter;

    public SettingsTemplateMyNoSqlRepository(IMyNoSqlServerDataWriter<SettingTemplateMyNoSqlModel> dataWriter)
    {
        _dataWriter = dataWriter;
    }

    public async Task InsertSettingsTemplateAsync(ISettingsTemplate src)
    {
        var insertModel = SettingTemplateMyNoSqlModel.Create(src);
        insertModel.CreateDate = DateTime.UtcNow;
        insertModel.LastUpdateDate = DateTime.UtcNow;
        await _dataWriter.InsertAsync(insertModel);
    }

    public async Task UpdateSettingsTemplateAsync(ISettingsTemplate src)
    {
        var insertModel = SettingTemplateMyNoSqlModel.Create(src);
        insertModel.LastUpdateDate = DateTime.UtcNow;
        await _dataWriter.InsertOrReplaceAsync(insertModel);
    }

    public async Task DeleteSettingsTemplateAsync(string env, string serviceName)
    {
        var pk = SettingTemplateMyNoSqlModel.GeneratePartitionKey(env);
        var rk = SettingTemplateMyNoSqlModel.GeneratePartitionKey(serviceName);
        await _dataWriter.DeleteAsync(pk, rk);
    }

    public async Task<IEnumerable<ISettingsTemplate>> GetAllSettingsTemplatesAsync()
    {
        return await _dataWriter.GetAsync();
    }

    public async Task<ISettingsTemplate> GetSettingsTemplateAsync(string env, string serviceName)
    {
        var pk = SettingTemplateMyNoSqlModel.GeneratePartitionKey(env);
        var rk = SettingTemplateMyNoSqlModel.GeneratePartitionKey(serviceName);
        return await _dataWriter.GetAsync(pk, rk);
    }
}