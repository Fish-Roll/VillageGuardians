using DarkRift.Client;

namespace Features.Network.Lobby
{
    public class LobbyInfo
    {
        public uint id;
        public uint hostId;
        public uint playerId;
        public string code;

        private DarkRiftClient _client;

        public LobbyInfo(DarkRiftClient client)
        {
            _client = client;
        }

        public LobbyInfo Create()
        {
            
        }

        public void Join()
        {
            
        }
        
        public void Leave()
        {
            
        }
    }
}