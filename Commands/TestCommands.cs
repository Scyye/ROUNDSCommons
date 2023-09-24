using Photon.Pun;
using ROUNDSCommons.Utils;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using UnityEngine;

namespace ROUNDSCommons.Commands
{
    public class TestCommands
    {
        public class KillCommand : Command
        {
            public override CommandDetails Details => new CommandDetails
            {
                Name = "kill",
                Description = "Kills the player",
                Aliases= new[] {"suicide"}
            };
        

            public override CommandResponse Execute(CommandEvent e)
            {
                e.executor.data.view.RPC("RPCA_Die", RpcTarget.MasterClient, new object[]
                            {
                                    new UnityEngine.Vector2(0, 1)
                            });

                e.executor.data.health = -1;
                e.executor.data.dead = true;


                if (e.executor.data.dead)
                {
                    return new CommandResponse()
                    {
                        Success = true,
                        Message = "Killed the player"
                    };
                }
                else
                {
                    return new CommandResponse()
                    {
                        Success = false,
                        Message = "Failed to kill the player"
                    };
                }
            }
        }

        public class KillBots : Command
        {
            public override CommandDetails Details => new CommandDetails()
            {
                Name = "kill",
                Description = "Kills all bots",
                Aliases = new[] { "kill-bots"}
            };

            public override CommandResponse Execute(CommandEvent e)
            {
                foreach (var player in PlayerManager.instance.players)
                {
                    if (player.transform.GetComponentInChildren<PlayerAI>().isActiveAndEnabled)
                    {
                        PlayerAssigner.instance.players.Remove(player.data);
                        PlayerManager.instance.RemovePlayer(player);
                    }
                }

                return new CommandResponse()
                {
                    Success = true,
                    Message = "Killed all bots"
                };
            }
        }
    }
}
