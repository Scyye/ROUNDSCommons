# ROUNDSCommons Commands
**PLEASE REPORT ANY BUGS OR GLITCHES IN THE [ROUNDS MODDING COMMUNITY DISCORD](https://discord.com/invite/rounds-modding), IN #bug-reports, AND PING @scyye**

### How to use commands
Commands are ran by using `!command`. 

There will be a help command shortly, for now, check the logs.


## Creation
This section assumes prexisting knowledge on creating ROUNDS mods,
for a tutorial on that, see [here](https://docs.google.com/document/d/1zu_89HeFC4aU9xI1MGXTkW1rDLnVCVfoQa5YiNpaWD8/)

### Importing dependencies
Just the same way you inport all the DLLs, import [ROUNDSCommons.dll](https://rounds.thunderstore.io/package/Scyye/ROUNDSCommonsAndConsole/) into your dependencies

And put:
```cs
[BepInDependency("dev.sub5allts.rounds.commons")]
```
above your main class.


### Creating Commands
To create a command, create a class that extends `Command`:
```cs
    public class ExampleCommand : Command
    {
    public override CommandDetails Details => new CommandDetails()
    {
        Name = "example",
        Description = "An example command",
        Scope = CommandScope.Sandbox,
        Aliases = new string[] {
            "command",
            "test"
        }
    };

    public override CommandResponse Execute(CommandEvent e)
    {
        Console.WriteLine("Hello World!");

        return new CommandResponse()
        {
            Success = true,
            Message = "Printed Hello World!",
        };
    }
}
```

#### Command Details
`Name` is the text that goes after "!" when running a command.

`Description` is the text that will show up in !help when that gets added.

`Scope` is the scope the command is valid in, either HOST, where only the host can do it, SANDBOX where you can do it in sandbox only, or GLOBAL, where you can do it globally.

`Aliases` (Optional) is equivelent commands that can be ran to do the same thing.

#### Execute()
Inside `Execute()` you can get the executor (`Player`), the arguments (`String[]`), and the details (`CommandDetails`) about the command.

`Execute()` returns a `CommandResponse`, these are fairly simple. Success (`bool`), did the command successfully run? And Message (`String`), the message to respond to the player with.


### Registering Commands
Simply put
```cs
CommonsPlugin.instance.commandManager.AddCommand(new ExampleCommand());
```
inside `Awake()`.

(Replace `new ExampleCommand()`, with `new YourCommandClass()`)


Make sure ROUNDSCommons is installed, add your mod to your profile, and enjoy your commands!