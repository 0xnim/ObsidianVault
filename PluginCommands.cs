using ObsidianVault;

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
}