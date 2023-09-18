using BepInEx;
using HarmonyLib;
using ROUNDSCommons.Commands;
using System.Collections.Generic;

namespace ROUNDSCommons
{
    // Tells BepIn what process the mod is using
    [BepInProcess("Rounds.exe")]
    [BepInPlugin(ModId, ModName, Version)]

    [BepInDependency("com.bosssloth.rounds.BetterChat", BepInDependency.DependencyFlags.HardDependency)]
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
            instance = this;

            commandManager = gameObject.AddComponent<CommandManager>();
            commandManager.AddCommand(new TestCommands.KillCommand());

            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }

        void Start()
        {
            int i = 0;
            foreach (var command in commandManager.AllCommands())
            {
                logger.Log($"Registering command {command.Details.Name}");
                i++;
            }
            logger.Log($"Registered {i} commands");
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
