using DarkRift.Client;
using UnityEngine;

namespace Features.Network.Lobby
{
    public class LobbyController
    {
        [SerializeField] private LobbyView view;
        private DarkRiftClient _client;
        
        private void Awake()
        {
            view.Init(Create, Join, Leave);
            _client = ConnectionManager.Instance.Client;
        }
        
        public void LobbyReceiver(object sender, MessageReceivedEventArgs messageReceived)
        {
            switch (messageReceived.Tag)
            {
                case (ushort)Tags.CreateLobby:
                    break;
                case (ushort)Tags.JoinLobby:
                    break;
                case (ushort)Tags.ExitLobby:
                    break;
            }
        }

        private void Create()
        {
            view.Lobby = new LobbyInfo();
        }

        private void Join()
        {
            view.Lobby.Join();
        }

        private void Leave()
        {
            view.Lobby.Leave();
        }
        
    }
}