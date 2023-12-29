using System;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEditor.Sprites;

namespace Online
{
    public class RemoteSocket
    {
        public Socket socket;
        public NetworkStream stream;

        public RemoteSocket(Socket socket)
        {
            this.socket = socket;
            if (socket.Connected)
                stream = new NetworkStream(socket);
        }

        public async Task Connect(string ip, int port)
        {
            await socket.ConnectAsync(ip, port);
            stream = new NetworkStream(socket);
        }

        public async Task<Packet> ReadPacket()
        {
            byte[] buffer = new byte[3];
            await stream.ReadAsync(buffer, 0, 3);
            byte id = buffer[0];
            int messageLength = BitConverter.ToUInt16(buffer, 1);


            buffer = new byte[messageLength];

            do
            {
                messageLength -= await stream.ReadAsync(buffer, 0, messageLength);
            } while (messageLength > 0);

            return new Packet(id, buffer);
        }

        public async Task<bool> SendPacket(Packet packet)
        {
            if (!socket.Connected) return false;
            await stream.WriteAsync(packet.GetData(), 0, packet.getDataLength());
            return true;
        }

        public bool Connected()
        {
            return socket.Connected;
        }

        public void Close()
        {
            stream.Close();
            socket.Close();
        }
    }
}