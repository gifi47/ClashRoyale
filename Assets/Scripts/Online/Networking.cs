#if DEBUG
#define SERVER
#endif
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Online
{
    public static class Networking
    {

#if SERVER
    public static Server server;

    public static Task sendData;
    public static List<Task> listen;

    //public static NetController netController;

    private static bool running = false;

    public static void Start()
    {
        running = true;
        server = new Server();
        server.OnConnect = OnConnect;
        server.Start();
        sendData = SendData();
    }

    public static async Task OnConnect(int clientId){
        //await server.Send(clientId, netController.GetData(-2));
        //netController.SpawnPlayer(clientId);
        //await server.Send(netController.GetData(clientId));
        AddTask(clientId);
    }

    public static void AddTask(int clientId)
    {
        Task.Run(()=>Listen(clientId));
    }

    public static void Close()
    {
        running = false;
        server.Close();
    }

    public static async Task SendData(){
        while (running){
            //await server.Send(netController.GetData(-1));
            await Task.Delay(10);
        }
    }

    public static async Task Listen(int clientId){
        while (running){
            //Console.WriteLine("Start receiving data");
            //netController.ReceiveData(clientId, await server.Read(clientId));
            //Console.WriteLine("Receiving data complited");
            await Task.Delay(10);
        }
    }
#elif CLIENT

        public static Client client;

        public static NetController netController;

        public static Task sendData;
        public static Task listen;

        public static bool running = false;

        public static string ip;
        public static int port;

        public static void SetConnectionInfo(string ip, int port)
        {
            Networking.ip = ip;
            Networking.port = port;
        }

        public static void Start(string ip, int port)
        {
            client = new Client();
            client.OnConnect = () =>
            {
                listen = Listen();
                sendData = SendData();
            };
            client.OnConnectFailure = () => { SceneManager.LoadScene("MainMenu"); };
            client.Connect(ip, port);
            running = true;

        }

        public static void Start()
        {
            client = new Client();
            client.OnConnect = () =>
            {
                listen = Listen();
                sendData = SendData();
            };
            client.OnConnectFailure = () => { SceneManager.LoadScene("MainMenu"); };
            client.Connect(ip, port);
            running = true;
            //netController.AddText("Start client");
        }

        public static async Task SendData()
        {
            while (running)
            {
                //netController.AddSendText("Wait for send data");
                await client.Send(netController.GetData());
                await Task.Delay(50);
            }
        }

        public static async Task Listen()
        {
            while (running)
            {
                //netController.AddReadText("Wait for read data");
                netController.ReceiveData(await client.Read());
                await Task.Delay(10);
            }
        }

        public static void Close()
        {
            client.Close();
        }

#endif

    }
}
