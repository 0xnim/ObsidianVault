namespace VaultPluginNamespace;

public interface IEconomy
{
    double GetBalance(string playerName);
    void AddBalance(string playerName, double amount);
    bool DeductBalance(string playerName, double amount);
}

public sealed class VaultApi
{
    private IEconomy economy;

    // Constructor - This could be swapped with different economy implementations
    public VaultApi(IEconomy economyPlugin)
    {
        economy = economyPlugin;
    }

    public double GetBalance(string playerName)
    {
        return economy.GetBalance(playerName);
    }

    public void AddBalance(string playerName, double amount)
    {
        economy.AddBalance(playerName, amount);
    }

    public bool DeductBalance(string playerName, double amount)
    {
        return economy.DeductBalance(playerName, amount);
    }
}