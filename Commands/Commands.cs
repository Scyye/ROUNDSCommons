using System;
using System.Collections.Generic;
using System.Linq;
using BetterChat;
using HarmonyLib;
using JetBrains.Annotations;
using Photon.Pun;
using ROUNDSCommons.Utils;

namespace ROUNDSCommons.Commands
{
    [HarmonyPatch(typeof(DevConsole))]
    public class DevConsoleSendPatch
    {
        [HarmonyPatch("Send")]
        public static bool Prefix(string message)
        {
            return !message.StartsWith("!");
        }

        [HarmonyPatch("SpawnCard")]
        [HarmonyPrefix]
        public static bool SpawnCardPatch(string message)
        {
            return !message.StartsWith("!");
        }
    }

    [HarmonyPatch(typeof(ChatMonoGameManager))]
    public class ConsoleUtils
    {
        [HarmonyPatch(nameof(ChatMonoGameManager.CreateLocalMessage))]
        [HarmonyPrefix]
        public static bool Prefix(string prefix, [CanBeNull] string groupName, string playerName, int colorID, string message, string visualGroupName = "", string objName = "")
        {
            CommonsPlugin.instance.logger.Log("message sent..." + " " + message);
            if (message.StartsWith("!"))
            {
                ExecuteCommand(message, PlayerUtils.GetPlayerFromName(playerName));
                return false;
            }
            return true;
        }

        


        public static Command.CommandResponse ExecuteCommand(string message, Player player)
        {
            string[] splitMessage = { message };
            if (message.Contains(" ")) { splitMessage = StringUtils.FuckingSplitAGodDamnStringWhyTheFuckIsStringDotSplitNotDefinedProperly(message, ' '); }
            


            string command = splitMessage[0].Replace("!","");
            string[] args = new string[splitMessage.Length];
            Array.Copy(splitMessage, 1, args, 0, splitMessage.Length-1);

            Command? c = CommonsPlugin.instance.commandManager.GetCommand(command);

            if (c == null)
                return Command.CommandResponse.DefaultNoSuccess;

            if (c.Details.Scope==Command.CommandScope.Host&&!player.data.view.Owner.IsMasterClient)
                return Command.CommandResponse.DefaultNoSuccess;

            if (c.Details.Scope == Command.CommandScope.Sandbox && !PhotonNetwork.OfflineMode)
                return Command.CommandResponse.DefaultNoSuccess;
                
            

            return c.Execute(new Command.CommandEvent
            {
                args = args,
                executor = player,
                Details = c.Details
            });
        }
    }

    public class CommandManager
    {
        private List<Command> commands = new List<Command>();

        public Command GetCommand(string command)
        {
            foreach (var cmd in commands)
            {
                if (cmd.Details.Name.Equals(command, StringComparison.OrdinalIgnoreCase) || cmd.Details.Aliases.ToList().Contains(command)) return cmd;
            }
            return null;
        }

        public void AddCommand(Command command) 
        {
            commands.Add(command);
        }

        public void RemoveCommand(Command command)
        {
            commands.Remove(command);
        }

        public List<Command> AllCommands()
        {
            return commands;
        }
    }

    public abstract class Command
    {
        public abstract CommandDetails Details { get; }

        public class CommandDetails
        {
            public string Name { get; set; }
            public string Description { get; set; } = "No command description";
            public CommandScope Scope { get; set; } = CommandScope.Sandbox;
            public string[] Aliases { get; set; } = new string[] {};
        }

        public class CommandResponse
        {
            public static CommandResponse DefaultNoSuccess
            {
                get => new CommandResponse() { Success = false, Message = "Didn't run the command" };
            }

            public static CommandResponse DefaultSuccess
            {
                get => new CommandResponse { Success = true, Message  = "Successfully ran the command!" };
            }


            public bool Success { get; set; }
            public string Message { get; set; }
        }
        
        public class CommandEvent
        {
            public CommandDetails Details { get; set; }
            public string[] args { get; set; }
            public Player executor { get; set; }

        }

        public enum CommandScope
        {
            Sandbox,
            Host,
            Global
        }
        public abstract CommandResponse Execute(CommandEvent e);

        
    }

    /*
    public class TestCommand : Command
    {
        public override CommandDetails Details => new CommandDetails
        {
            Name = "heal"
        };

        public override CommandResponse Execute(CommandEvent e)
        {
            CommonsPlugin.instance.logger.Log($"Healed {e.executor.data.view.Owner.NickName} for 25f");
            e.executor.data.healthHandler.Heal(25f);
            return CommandResponse.DefaultSuccess;
        }
    }
    */
}
