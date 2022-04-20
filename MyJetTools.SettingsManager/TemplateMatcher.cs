using System.Text.RegularExpressions;
using MyJetTools.SettingsManager.Abstractions;

namespace MyJetTools.SettingsManager;

public class TemplateMatcher
{
    private readonly ISecretValuesStore _secretValuesStore;

    public TemplateMatcher(ISecretValuesStore secretValuesStore)
    {
        _secretValuesStore = secretValuesStore;
    }

    public async Task<string> MatchTemplate(string yamlSettings)
    {
        var secretRegex = new Regex(@"\${(.*?)}");
        var result = secretRegex.Matches(yamlSettings);

        foreach (var match in result)
        {
            var asString = match.ToString()?
                .Replace("${", "")
                .Replace("}", "");

            var secretValue = await _secretValuesStore.GetSecretValueAsync(asString);
            var patternString = "${" + asString + "}";
            yamlSettings = yamlSettings.Replace(patternString, secretValue);
        }

        return yamlSettings;
    }
}