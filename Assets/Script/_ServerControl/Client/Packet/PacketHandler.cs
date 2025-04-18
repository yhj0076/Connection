using Script._ServerControl.Session;
using ServerCore;
using UnityEngine;

namespace Script._ServerControl.Client.Packet
{
    public class PacketHandler
    {
        public static void S_JoinGameRoomHandler(PacketSession session, IPacket packet)
        {
            S_JoinGameRoom joinGameRoom = packet as S_JoinGameRoom;
            ServerSession serverSession = session as ServerSession;
            
            GameObject lobby = GameObject.Find("LobbyControl");
            if (lobby != null)
            {
                lobby.GetComponent<MultiLobbyManager>().enemyExist = joinGameRoom.EnemyIsExist;
            }
        }

        public static void S_BroadcastGameStartHandler(PacketSession session, IPacket packet)
        {
            S_BroadcastGameStart broadcastGameStart = packet as S_BroadcastGameStart;
            ServerSession serverSession = session as ServerSession;
        }

        public static void S_BroadcastEndGameHandler(PacketSession session, IPacket packet)
        {
            S_BroadcastEndGame endGame = packet as S_BroadcastEndGame;
            ServerSession serverSession = session as ServerSession;
            serverSession.DisConnect();
        }

        public static void S_BroadcastLeaveGameHandler(PacketSession session, IPacket packet)
        {
            S_BroadcastLeaveGame leaveGame = packet as S_BroadcastLeaveGame;
            ServerSession serverSession = session as ServerSession;
        }

        public static void S_AttackResultHandler(PacketSession session, IPacket packet)
        {
            S_AttackResult attackResult = packet as S_AttackResult;
            ServerSession serverSession = session as ServerSession;
            
            GameObject healthManager = GameObject.Find("HealthManager");
            if (healthManager != null )
            {
                healthManager.GetComponent<MultiHealthManager>().Damage = attackResult.dmg;
            }
        }

        public static void S_BroadCastGainedDmgHandler(PacketSession session, IPacket packet)
        {
            S_BroadCastGainedDmg gainedDmg = packet as S_BroadCastGainedDmg;
            ServerSession serverSession = session as ServerSession;

            // Console.WriteLine($"\tPlayer1 gainedDmg : {gainedDmg.HostGainedDmg}\n" +
            //                   $"\tPlayer2 gainedDmg : {gainedDmg.GuestGainedDmg}\n");
            
            GameObject player = GameObject.Find("Player");
            GameObject enemyPlayer = GameObject.Find("EnemyPlayer");
            if (player != null && enemyPlayer != null)
            {
                // player.GetComponent<MultiHealthManager>().GainedDmg = gainedDmg.HostGainedDmg;
            }
        }
    }
}