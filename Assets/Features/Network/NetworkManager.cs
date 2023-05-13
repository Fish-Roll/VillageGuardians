using System.Net;
using Features.Network.Abstract;
using Features.Network.Client;
using Features.Network.Lobby;
using UnityEngine;

namespace Features.Network
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] private string ip;
        [SerializeField] private int port;

        private AbstractMessageHandler _messageHandler;
        private ConnectionManager _connectionManager;
        private LobbyController _lobbyController;

        private void Awake()
        {
            _connectionManager = ConnectionManager.Instance;
            _messageHandler = new ClientMessageHandler(_connectionManager.Client);
            _connectionManager.ConnectWithLog(IPAddress.Parse(ip), port);

            _connectionManager.Client.MessageReceived += _lobbyController.;
            _connectionManager.Client.MessageReceived += _messageHandler.MessageReceiver;
        }
    }
}
