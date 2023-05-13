using DarkRift.Client;

namespace Features.Network.Abstract
{
    public abstract class AbstractMessageHandler
    {
        protected readonly DarkRiftClient client;

        protected AbstractMessageHandler(DarkRiftClient client)
        {
            this.client = client;
        }

        public abstract void MessageReceiver(object sender, MessageReceivedEventArgs messageReceived);
    }
}