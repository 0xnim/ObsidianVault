using Microsoft.Extensions.Logging;
using Obsidian.API;
using Obsidian.API.Commands;
using Obsidian.API.Plugins;
using System.Threading.Tasks;

namespace ObsidianVault;

//All command modules are created with a scoped lifetime
[CommandGroup("vault")]
public class Vault : CommandModuleBase
{
    [Inject]
    public ILogger<Vault> Logger { get; set; }
    
    [RequirePermission(PermissionCheckType.Any, true, "vault.version", "vault.admin")]
    [Command("version")]
    [CommandInfo("Display the plugin version.")]
    public async Task VersionCommandAsync()
    {
        await this.Player.SendMessageAsync($"Vault plugin version: {this.Plugin.Info.Version}");
    }
    /*
    [RequirePermission(PermissionCheckType.Any, true, "vault.admin", "vault.reload")]
    [Command("reload")]
    [CommandInfo("Reload the plugin.")]
    public async Task ReloadCommandAsync()
    {
        await this.Player.SendMessageAsync("Reloading plugin...");
        // TODO: Reload the plugin
        await this.Player.SendMessageAsync("Plugin reloaded.");
    }
    */
}
