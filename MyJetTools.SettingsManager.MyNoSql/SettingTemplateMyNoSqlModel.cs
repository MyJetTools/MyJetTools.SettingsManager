using MyJetTools.SettingsManager.Abstractions;
using MyNoSqlServer.Abstractions;

namespace MyJetTools.SettingsManager.MyNoSql;

public class SettingTemplateMyNoSqlModel : MyNoSqlDbEntity, ISettingsTemplate
{
    public const string TableName = "settingsTemplate";
    
    public static string GeneratePartitionKey(string env)
    {
        return env;
    }
    
    public static string GenerateRowKey(string serviceName)
    {
        return serviceName;
    }
    public string Env => PartitionKey;
    public string ServiceName => RowKey;
    public string SettingsYamlTemplate { get; set;}
    public DateTime CreateDate { get; set;}
    public DateTime LastUpdateDate { get; set;}

    public static SettingTemplateMyNoSqlModel Create(ISettingsTemplate src)
    {
        return new SettingTemplateMyNoSqlModel
        {
            PartitionKey = GeneratePartitionKey(src.Env),
            RowKey = GenerateRowKey(src.ServiceName),
            SettingsYamlTemplate = src.SettingsYamlTemplate,
            LastUpdateDate = src.LastUpdateDate,
            CreateDate = src.CreateDate
        };
    }
    
    
}