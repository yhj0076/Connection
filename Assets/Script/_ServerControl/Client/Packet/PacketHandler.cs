using DummyClient;
using ServerCore;
using UnityEngine;

public class PacketHandler
{
    public static void S_BroadcastEnterGameHandler(PacketSession session, IPacket packet)
    {
        S_BroadcastEnterGame broadcastEnterGamePacket = packet as S_BroadcastEnterGame;
        ServerSession serverSession = session as ServerSession;
        
        PlayerManager.Instance.EnterGame(broadcastEnterGamePacket);
    }
    
    public static void S_BroadcastLeaveGameHandler(PacketSession session, IPacket packet)
    {
        S_BroadcastLeaveGame broadcastLeaveGamePacket = (S_BroadcastLeaveGame)packet;
        ServerSession serverSession = session as ServerSession;
        
        PlayerManager.Instance.LeaveGame(broadcastLeaveGamePacket);
    }
    
    public static void S_PlayerListHandler(PacketSession session, IPacket packet)
    {
        S_PlayerList playerListPacket = (S_PlayerList)packet;
        ServerSession serverSession = session as ServerSession;
        
        PlayerManager.Instance.Add(playerListPacket);
    }
    
    public static void S_BroadcastMoveHandler(PacketSession session, IPacket packet)
    {
        S_BroadcastMove broadcastMovePacket = (S_BroadcastMove)packet;
        ServerSession serverSession = session as ServerSession;
        
        PlayerManager.Instance.Move(broadcastMovePacket);
    }
}