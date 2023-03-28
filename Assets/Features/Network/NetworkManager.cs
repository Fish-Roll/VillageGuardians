using System.Net;
using UnityEngine;

namespace Features.Network
{
    public class NetworkManager : MonoBehaviour
    {
        [SerializeField] private string ip;
        [SerializeField] private int port;

        private BaseMessageHandler _messageHandler;

        private ConnectionManager _connectionManager;
        // Start is called before the first frame update
        private void Awake()
        {
            _messageHandler = new BaseMessageHandler();
            _connectionManager = ConnectionManager.Instance;
            _connectionManager.ConnectWithLog(IPAddress.Parse(ip), port);
            _connectionManager.Client.MessageReceived += _messageHandler.MessageReceiver;
        }

        private void Start()
        {
            _messageHandler.SendTestData(Tags.TestTag, "TestData");
        }
    }
}
