using System;
using System.Net;
using ServerCore;

namespace Script._ServerControl.Session
{
    public class ServerSession : PacketSession
    {
        public int SessionId { get; set; }
        
        public override void OnConnected(EndPoint endPoint)
        {
            // Console.WriteLine($"Connected to {endPoint}");
            // this.Send(pong.Write());
        }

        public override void OnSend(int numOfBytes)
        {
            // throw new NotImplementedException();
            // Console.WriteLine($"Sending {numOfBytes} bytes");
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            // Console.WriteLine($"Disconnected from {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer, (session, packet) =>
            {
                PacketQueue.Instance.Push(packet);
            });
        }
    }
}