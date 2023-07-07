using BepInEx;
using HarmonyLib;
using ROUNDSCommons.Commands;

namespace ROUNDSCommons
{
    // Tells BepIn what process the mod is using
    [BepInProcess("Rounds.exe")]
    [BepInPlugin(ModId, ModName, Version)]
    public class CommonsPlugin : BaseUnityPlugin
    {
        private const string ModId = "dev.sub5allts.rounds.commons";
        private const string ModName = "Commons";
        private const string Version = "1.0.0";

        public static CommonsPlugin instance { get; private set; }

        public Logger logger { get; private set; }

        public CommandManager commandManager { get; private set; }

        void Awake()
        {
            logger = new Logger(ModName);
            commandManager = new CommandManager();
            instance = this;

            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            // commandManager.AddCommand(new TestCommand());
            commandManager.AddCommand(new ClearBotsCommand());
            commandManager.AddCommand(new CardCommands.GiveCard());
        }
    }

    public class Logger 
    {
        string ModName;

        public Logger(string ModName) 
        {
            this.ModName = ModName;
        }


        private string FormatLog(string log)
        {
            return $"[{ModName}] {log}";
        }

        public void Log(string message)
        {
            UnityEngine.Debug.Log(FormatLog(message));
        }
        public void LogError(string error)
        {
            Debug.LogError(FormatLog(error));
        }

        public void LogWarning(string warning)
        {
            Debug.LogWarning(FormatLog(warning));
        }
    }
}
