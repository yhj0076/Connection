using Script._ServerControl.Session;
using ServerCore;
using TMPro;
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
                serverSession.SessionId = joinGameRoom.MySessionId;
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
            if (serverSession.SessionId == endGame.WinnerId)
            {
                SecurityPlayerPrefs.SetString("isWin", "true");
            }
            else
            {
                SecurityPlayerPrefs.SetString("isWin", "false");
            }

            serverSession.DisConnect();
            GameManager gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gameManager.MultiEnd();
        }

        public static void S_BroadcastLeaveGameHandler(PacketSession session, IPacket packet)
        {
            S_BroadcastLeaveGame leaveGame = packet as S_BroadcastLeaveGame;
            ServerSession serverSession = session as ServerSession;
            
            Debug.Log("Player Left");
            // serverSession.DisConnect();
            
            MultiStage stage = GameObject.Find("Stage").GetComponent<MultiStage>();

            if (stage is not null)
            {
                stage.state = GameState.ConnectionFailed;
            } 
        }

        public static void S_AttackResultHandler(PacketSession session, IPacket packet)
        {
            S_AttackResult attackResult = packet as S_AttackResult;
            ServerSession serverSession = session as ServerSession;
            
            GameObject healthManager = GameObject.Find("HealthManager");
            MultiStageManager stageManager = GameObject.Find("StageManager").GetComponent<MultiStageManager>();
            if (healthManager is not null )
            {
                healthManager.GetComponent<MultiHealthManager>().Calculate(attackResult.dmg);
                stageManager.UpdateData();
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
                Debug.Log("StageManager is not null");
                stageManager.GetComponent<MultiStageManager>().PlayerPower = gainedDmg.HostGainedDmg;
                stageManager.GetComponent<MultiStageManager>().EnemyPower = gainedDmg.GuestGainedDmg;
                stageManager.GetComponent<MultiStageManager>().UpdateData();
            }
            else
                Debug.Log("StageManager is null");
        }

        public static void S_TimerHandler(PacketSession session, IPacket packet)
        {
            S_Timer timer = packet as S_Timer;
            
            MultiStageManager stageManager = GameObject.Find("StageManager").GetComponent<MultiStageManager>();
            if (stageManager != null)
            {
                stageManager.LeftTime = timer.second;
                stageManager.timeText.GetComponent<TextMeshProUGUI>().text = timer.second.ToString();
            }
        }
    }
}