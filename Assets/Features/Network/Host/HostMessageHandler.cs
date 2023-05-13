using DarkRift.Client;
using Features.Network.Client;

namespace Features.Network.Host
{
    public class HostMessageHandler : ClientMessageHandler
    {
        public HostMessageHandler(DarkRiftClient client) : base(client)
        {
            
        }
        
        public override void MessageReceiver(object sender, MessageReceivedEventArgs messageReceiver)
        {
            switch (messageReceiver.Tag)
            {
                case (ushort)Tags.CreateLobby:
                    
                    break;
                case (ushort)Tags.ExitLobby:
                    
                    break;
                case (ushort)Tags.JoinLobby:
                    
                    break;
            }
        }
    }
}