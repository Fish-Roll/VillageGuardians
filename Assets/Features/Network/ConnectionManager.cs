using System.Net;
using DarkRift;
using DarkRift.Client;
using UnityEngine;

namespace Features.Network
{
    public class ConnectionManager
    {
        private static ConnectionManager _instance;
        public DarkRiftClient Client { get; }
        public static ConnectionManager Instance => _instance ??= new ConnectionManager();

        private ConnectionManager()
        {
            Client = new DarkRiftClient();
        }

        public void ConnectWithLog(IPAddress ip, int port, bool noDelay = false)
        {
            if (Client.ConnectionState is ConnectionState.Connected or ConnectionState.Connecting)
                return;
            Client.ConnectInBackground(ip, port, noDelay, (e) =>
            {
                if (e == null)
                {
                    Debug.Log($"Client {Client.ID} successful connected");
                    return;
                }

                Debug.LogWarning(e.Message);
            });
        }
    }
}
