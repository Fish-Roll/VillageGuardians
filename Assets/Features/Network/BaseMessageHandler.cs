using System;
using DarkRift;
using DarkRift.Client;
using UnityEngine;

namespace Features.Network
{
    public class BaseMessageHandler
    {
        private DarkRiftClient _client;

        public BaseMessageHandler()
        {
            _client = ConnectionManager.Instance.Client;
        }
        public virtual void MessageReceiver(object sender, MessageReceivedEventArgs messageReceived)
        {
            switch (messageReceived.Tag)
            {
                case (ushort)Tags.TestTag:
                    GetTestData(sender, messageReceived);
                    break;
            }
        }

        public void SendTestData(Tags tag, string data)
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write(data);
                using (Message message = Message.Create((ushort)tag, writer))
                {
                    _client.SendMessage(message, SendMode.Reliable);
                }
            }
        }

        private void GetTestData(object sender, MessageReceivedEventArgs messageReceived)
        {
            using (Message message = messageReceived.GetMessage())
            {
                using (DarkRiftReader reader = message.GetReader())
                {
                    Debug.Log($"Get {_client.ID} --- {reader.ReadString()}");
                }
            }
        }
    }
}