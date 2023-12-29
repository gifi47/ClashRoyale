using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Online
{
    public class Client
    {
        RemoteSocket remoteSocket;

        public Client()
        {
            remoteSocket = new RemoteSocket(new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp));
        }

        public delegate void Action();
        public Action OnConnect;
        public Action OnConnectFailure;

        public async Task<Packet> Read()
        {
            return await remoteSocket.ReadPacket();
        }

        public async Task Send(Packet data)
        {
            await remoteSocket.SendPacket(data);
        }

        public async void Connect(string ip, int port)
        {
            try
            {
                Console.WriteLine("start connecting");
                await remoteSocket.Connect(ip, port);
                Console.WriteLine("success");
                OnConnect?.Invoke();
            }
            catch (SocketException)
            {
                Console.WriteLine("ff");
                OnConnectFailure?.Invoke();
            }
        }

        public void Close()
        {
            remoteSocket.Close();
        }
    }
}
