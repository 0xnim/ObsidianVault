using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace ObsidianVault;

public class VaultPlugin : PluginBase
{
    [Inject]
    public ILogger<VaultPlugin> Logger { get; set; }

    private readonly string _fileLocation = "economy.json";

    private VaultApi EconomyApi { get; set; }
    
    public override void ConfigureRegistry(IPluginRegistry registry)
    {
        registry.MapCommands();
    }

    public override void ConfigureServices(IServiceCollection services)
    {
        EconomyApi = new VaultApi();
        services.AddSingleton<VaultApi>(EconomyApi);
    }

    public override ValueTask OnLoadedAsync(IServer server)
    {
        Logger.LogInformation("VaultPlugin loaded!");
        return default;
    }

    public override async ValueTask OnServerReadyAsync(IServer server)
    {
        await LoadEconomyAsync(_fileLocation);
        Logger.LogInformation("VaultPlugin ready!");
    }

    public override async ValueTask OnUnloadingAsync()
    {
        Logger.LogInformation("VaultPlugin unloading ...!");
        await SaveEconomyAsync(_fileLocation);
        Logger.LogInformation("VaultPlugin unloaded!");
    }

    private async Task LoadEconomyAsync(string filePath)
    {
        if (File.Exists(filePath))
        {
            try
            {
                var json = await File.ReadAllTextAsync(filePath);
                var data = JsonSerializer.Deserialize<Dictionary<string, double>>(json);
                if (data != null)
                {
                    EconomyApi.LoadData(data);
                    Logger.LogInformation("Economy data loaded.");
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Error loading economy data: {ex.Message}");
            }
        }
        else
        {
            Logger.LogWarning("Economy data file not found. Starting fresh.");
        }
    }

    private async Task SaveEconomyAsync(string filePath)
    {
        try
        {
            var data = EconomyApi.GetData();
            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }
            await File.WriteAllTextAsync(filePath, json);
            Logger.LogInformation("Economy data saved.");
        }
        catch (Exception ex)
        {
            Logger.LogError($"Error saving economy data: {ex.Message}");
        }
    }
}
