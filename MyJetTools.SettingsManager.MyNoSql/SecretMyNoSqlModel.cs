using MyJetTools.SettingsManager.Abstractions;
using MyNoSqlServer.Abstractions;

namespace MyJetTools.SettingsManager.MyNoSql;

public class SecretMyNoSqlModel : MyNoSqlDbEntity ,ISecretModel
{
    public const string TableName = "settingsSecrets";

    public static string GeneratePartitionKey()
    {
        return "SettingsSecrets";
    }
    
    public static string GenerateRowKey(string secretName)
    {
        return secretName;
    }

    public string Name => RowKey;
    public string Value { get; set; }
    public DateTime LastUpdate { get; set; }
    public DateTime CreateDate { get; set; }

    public static SecretMyNoSqlModel Create(ISecretModel src)
    {
        return new SecretMyNoSqlModel
        {
            PartitionKey = GeneratePartitionKey(),
            RowKey = GenerateRowKey(src.Name),
            Value = string.Empty,
            LastUpdate = src.LastUpdate,
            CreateDate = src.CreateDate,
        };
    }
}