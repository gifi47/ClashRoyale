using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Net;
using System.Threading.Tasks;

namespace Online
{
    public class Server
    {
        IPEndPoint ipPoint;
        Socket socket;

        List<RemoteSocket> clients = new List<RemoteSocket>();

        Task listenClient;
        bool stop = false;

        public delegate Task Action(int clientId);
        public Action OnConnect;

        public Server()
        {
            ipPoint = new IPEndPoint(IPAddress.Any, 8888);
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipPoint);
            socket.Listen(255);
            Console.WriteLine($"Server started on {ipPoint.Address}:{ipPoint.Port}...");
        }

        public void Start()
        {
            Console.WriteLine("Start() server");
            listenClient = Listen();
        }

        public async Task Listen()
        {
            while (!stop)
            {
                Console.WriteLine($"Start listening...");
                Socket clientSocket = await socket.AcceptAsync();
                Console.WriteLine($"New connection from {clientSocket.RemoteEndPoint}");
                clients.Add(new RemoteSocket(clientSocket));
                Console.WriteLine($"clients count={clients.Count}");
                await OnConnect(clients.Count - 1);
            }
            Console.WriteLine($"Stop listening...");
        }

        public async Task<Packet> Read(int clientId)
        {
            //Console.WriteLine($"Read data from client id={clientId}");
            return await clients[clientId].ReadPacket();
        }

        public async Task Send(int clientId, Packet packet)
        {
            if (clientId < clients.Count)
            {
                Console.WriteLine($"Send to new client client_id={clientId}");
                await clients[clientId].SendPacket(packet);
            }
        }

        public async Task Send(Packet packet)
        {
            int currentClientsCount = clients.Count;
            //Console.WriteLine($"Send to clients count={currentClientsCount}");
            List<Task<bool>> tasks = new List<Task<bool>>();
            for (int i = 0; i < currentClientsCount; i++)
            {
                tasks.Add(clients[i].SendPacket(packet));
            }
            await Task.WhenAll(tasks);
            //Console.WriteLine($"Sended");
        }

        public void Close()
        {
            stop = true;
            foreach (RemoteSocket client in clients) client.Close();
            socket.Close();
        }
    }
}
