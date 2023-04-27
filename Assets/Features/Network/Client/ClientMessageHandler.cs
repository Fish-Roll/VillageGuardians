using DarkRift;
using DarkRift.Client;
using UnityEngine;

namespace Features.Network.Client
{
    public class ClientMessageHandler
    {
        private readonly DarkRiftClient _client;
        public ClientMessageHandler(DarkRiftClient client)
        {
            _client = client;
        }
        public virtual void MessageReceiver(object sender, MessageReceivedEventArgs messageReceived)
        {
            
        }
    }
}