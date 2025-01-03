using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Obsidian.API;
using Obsidian.API.Plugins;

namespace ObsidianVault;

public class VaultPlugin : PluginBase
{
    [Inject]
    public ILogger<VaultPlugin> Logger { get; set; }
    private readonly string _fileLocation = "economy.json";
    public VaultApi EconomyApi { get; private set; }
    
    public override void ConfigureRegistry(IPluginRegistry registry)
    {
        registry.MapCommands();
    }
    
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<VaultApi>();
    }

    public async ValueTask OnLoadAsync(IServer server)
    {
        Logger.LogInformation("VaultPlugin loading ...!");

        EconomyApi = new VaultApi();
        await LoadEconomyAsync(_fileLocation);
        
        Logger.LogInformation("VaultPlugin loaded!");
    }
    
    public async ValueTask OnUnloadAsync()
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
            if (directory != null)
            {
                Directory.CreateDirectory(directory); // Ensure directory exists
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
