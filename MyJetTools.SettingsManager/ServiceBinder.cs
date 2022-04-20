using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using MyJetTools.SettingsManager.Abstractions;
using MyJetTools.SettingsManager.MyNoSql;
using MyJetTools.SettingsManager.Services;
using MyNoSql.Sdk;

namespace MyJetTools.SettingsManager;

public static class ServiceBinder
{
    public static void BindServices(this IServiceCollection sc, SettingsAccessor accessor)
    {
        sc.AddSingleton<ISecretRepository, SecretsMyNoSqlRepository>();
        sc.AddSingleton<ISecretValuesStore, AzureKeyValuesSecretsStore>();
        sc.AddSingleton<TemplateMatcher>();
        sc.AddSingleton<ISettingsTemplateRepository, SettingsTemplateMyNoSqlRepository>();
        sc.AddTransient(_ =>
            new SecretClient(new Uri(accessor.GetSettings().KeyVaultUrl), new DefaultAzureCredential()));
    }

    public static void BindNoSql(this IServiceCollection sc, SettingsAccessor accessor)
    {
        sc.RegisterMyNoSqlWriter<SecretMyNoSqlModel>(() => accessor.GetSettings().MyNoSqlWriterUrl,
            SecretMyNoSqlModel.TableName);

        sc.RegisterMyNoSqlWriter<SettingTemplateMyNoSqlModel>(() => accessor.GetSettings().MyNoSqlWriterUrl,
            SettingTemplateMyNoSqlModel.TableName);
    }
}