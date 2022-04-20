using MyJetTools.SettingsManager.Abstractions;

namespace MyJetTools.SettingsManager.Forms.Secrets;

public class AddSecretFormModel : ISecretModel
{
    public string Name { get; set; }
    public string Value { get; set; }
    public DateTime LastUpdate { get; set; }
    public DateTime CreateDate { get; set; }

    public static AddSecretFormModel Create(ISecretModel src)
    {
        return new AddSecretFormModel
        {
            Name = src.Name,
            Value = src.Value,
            LastUpdate = src.LastUpdate,
            CreateDate = src.CreateDate
        };
    }
}