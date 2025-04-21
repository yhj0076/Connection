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
                if(joinGameRoom.EnemyIsExist)
                    Debug.Log("EnemyFound");
                else
                    Debug.Log("No Enemy Exist");
            }
        }

        public static void S_BroadcastGameStartHandler(PacketSession session, IPacket packet)
        {
            S_BroadcastGameStart broadcastGameStart = packet as S_BroadcastGameStart;
            ServerSession serverSession = session as ServerSession;
            GameObject lobby = GameObject.Find("LobbyControl");
            if (lobby != null)
            {
                lobby.GetComponent<MultiLobbyManager>().enemyExist = true;
                Debug.Log("Start");
            }
        }

        public static void S_BroadcastEndGameHandler(PacketSession session, IPacket packet)
        {
            S_BroadcastEndGame endGame = packet as S_BroadcastEndGame;
            ServerSession serverSession = session as ServerSession;
            Debug.Log($"Winner : {endGame.WinnerId}");
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
            if (healthManager is not null )
            {
                healthManager.GetComponent<MultiHealthManager>().Calculate(attackResult.dmg);
            }
        }

        public static void S_BroadCastGainedDmgHandler(PacketSession session, IPacket packet)
        {
            S_BroadCastGainedDmg gainedDmg = packet as S_BroadCastGainedDmg;
            ServerSession serverSession = session as ServerSession;

            // Console.WriteLine($"\tPlayer1 gainedDmg : {gainedDmg.HostGainedDmg}\n" +
            //                   $"\tPlayer2 gainedDmg : {gainedDmg.GuestGainedDmg}\n");
            
            GameObject stageManager = GameObject.Find("StageManager");
            if (stageManager != null)
            {
                stageManager.GetComponent<MultiStageManager>().PlayerPower = gainedDmg.HostGainedDmg;
                stageManager.GetComponent<MultiStageManager>().EnemyPower = gainedDmg.GuestGainedDmg;
            }
        }
    }
}