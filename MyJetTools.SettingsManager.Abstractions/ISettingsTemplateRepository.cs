namespace MyJetTools.SettingsManager.Abstractions;

public interface ISettingsTemplateRepository
{
    Task InsertSettingsTemplateAsync(ISettingsTemplate src);
    Task UpdateSettingsTemplateAsync(ISettingsTemplate src);
    Task DeleteSettingsTemplateAsync(string env, string serviceName);
    Task<IEnumerable<ISettingsTemplate>> GetAllSettingsTemplatesAsync();
    Task<ISettingsTemplate> GetSettingsTemplateAsync(string env, string serviceName);
}