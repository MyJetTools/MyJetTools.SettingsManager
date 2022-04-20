# MyJetTools.SettingsManager

The service allow you to setup settings service with Http Yaml access

# Get started

Env Variables:

| Variable            | Description                     |
|---------------------|---------------------------------|
| SETTINGS_PATH       | Path to settings file in Docker |
| AZURE_CLIENT_SECRET | Azure Key Vault client secret   |
| AZURE_TENANT_ID     | Azure Key Vault tenant id       |
| AZURE_CLIENT_ID     | Azure Key Vault id              |

### Settings

Need to create settings.yaml:

| Variable            | Description               |
|---------------------|---------------------------|
| key_vault_url       | Key vault url             |
| my_no_sql_writer_url | My no sql writer          |
