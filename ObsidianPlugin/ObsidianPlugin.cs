using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Obsidian.API;
using Obsidian.API.Plugins;

namespace VaultPluginNamespace;

public class VaultPlugin : PluginBase
{
    [Inject]
    public ILogger<VaultPlugin> Logger { get; set; }
    
    public override void ConfigureRegistry(IPluginRegistry registry)
    {
        registry.MapCommands();
    }
    

    public async ValueTask OnLoadAsync(IServer server)
    {
        Logger.LogInformation("VaultPlugin loaded!");
    }
    
    public async ValueTask OnUnloadAsync()
    {
        Logger.LogInformation("VaultPlugin unloaded!");
    }
    
    
}
