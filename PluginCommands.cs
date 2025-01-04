namespace ObsidianVault;

//All command modules are created with a scoped lifetime
[CommandGroup("mygroupCommand")]
public class PluginCommands : CommandModuleBase
{
    [Inject] public ILogger<PluginCommands> Logger { get; set; }

    [Command("subtract")]
    [CommandInfo("woop dee doo this command is from a plugin")]
    public async Task Subtract(int a, int b)
    {
        Logger.LogInformation("Subtracted {a}-{b}={c}", a, b, a - b);
        await this.Player.SendMessageAsync($"Subtracted {a}-{b}={a - b}");
    }

    [Command("add")]
    [CommandInfo("woop dee doo this command is from a plugin")]
    public async Task Add(int a, int b)
    {
        Logger.LogInformation("Added {a}+{b}={c}", a, b, a + b);
        await this.Player.SendMessageAsync($"Added {a}+{b}={a + b}");
    }
}