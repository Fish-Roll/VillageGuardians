using DarkRift;
using DarkRift.Client;
using Features.Network.Abstract;
using UnityEngine;

namespace Features.Network.Client
{
    public class ClientMessageHandler : AbstractMessageHandler
    {
        public ClientMessageHandler(DarkRiftClient client) : base(client)
        {
            
        }
        public override void MessageReceiver(object sender, MessageReceivedEventArgs messageReceived)
        {
            switch (messageReceived.Tag)
            {
                
            }
        }
    }
}