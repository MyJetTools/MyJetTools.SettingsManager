namespace MyJetTools.SettingsManager.Abstractions;

public interface ISecretModel
{
    string Name { get; }
    string Value { get; }
    DateTime LastUpdate { get; }
    DateTime CreateDate { get; }
}

public class SecretModel : ISecretModel
{
    public string Name { get; set; }
    public string Value { get; set; }
    public DateTime LastUpdate { get; set; }
    public DateTime CreateDate { get; set; }

    public static SecretModel Create(ISecretModel src)
    {
        return new SecretModel
        {
            Name = src.Name,
            Value = src.Value,
            CreateDate = src.CreateDate,
            LastUpdate = src.LastUpdate,
        };
    }
}