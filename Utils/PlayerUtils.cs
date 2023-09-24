using System;
using System.Collections.Generic;
using System.Text;
using Photon.Pun;
using UnityEngine;

namespace ROUNDSCommons.Utils
{
    public static class PlayerUtils
    {
        public static Photon.Realtime.Player GetPhotonPlayer(Player player)
        {
            return player.data.view.Owner;
        }

        public static Player GetPlayerFromPhoton(this Photon.Realtime.Player player)
        {
            return GetPlayerFromName(player.NickName);
        }

        public static Player GetPlayerFromName(string name)
        {
            Player player = PlayerManager.instance.players.Find(player => player.data.view.Owner.IsLocal);
            foreach (var p in PlayerManager.instance.players)
            {
                if (GetPhotonPlayer(p).NickName.Equals(name))
                {
                    return p;
                }
            }

            return player;
        }

        public static Player? GetPlayerWithActorID(int actorID)
        {
            List<Player> players = PlayerManager.instance.players;

            for (int index = 0; index < players.Count; ++index)
            {
                if (players[index].data.view.OwnerActorNr == actorID)
                    return players[index];
            }

            return null;
        }

        public static Player GetLocalPlayer() => GetPlayerWithActorID(PhotonNetwork.LocalPlayer.ActorNumber);
    }
}
