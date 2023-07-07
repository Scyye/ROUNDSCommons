using ROUNDSCommons.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace ROUNDSCommons.Commands
{
    public class ClearBotsCommand : Command
    {
        public override CommandDetails Details => new CommandDetails() 
        { 
            Name = "clear-bots",
            Aliases = new[]
            {
                "kill-bots",
                "kick-bots"
            }
        };

        public override CommandResponse Execute(CommandEvent e)
        {
            RemoveBots();
            return CommandResponse.DefaultSuccess;
        }

        private void RemoveBots()
        {
            Player localPlayer = PlayerUtils.GetLocalPlayer();

            foreach (Player player in PlayerManager.instance.players)
            {
                if (player != localPlayer)
                {
                    PlayerAssigner.instance.players.Remove(player.data);
                    PlayerManager.instance.RemovePlayer(player);
                }
            }
            if (PlayerManager.instance.players.Count > 1)
            {
                RemoveBots();
            }
        }
    }


}
