using System.Net;
using Features.Network.Client;
using UnityEngine;

namespace Features.Network
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] private string ip;
        [SerializeField] private int port;

        private ClientMessageHandler _messageHandler;

        private ConnectionManager _connectionManager;

        private void Awake()
        {
            _connectionManager = ConnectionManager.Instance;
            _messageHandler = new ClientMessageHandler(_connectionManager.Client);
            _connectionManager.ConnectWithLog(IPAddress.Parse(ip), port);
            _connectionManager.Client.MessageReceived += _messageHandler.MessageReceiver;
        }
    }
}
