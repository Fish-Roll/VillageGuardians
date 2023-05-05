using System;
using System.Collections;
using DarkRift;
using DarkRift.Client;
using UnityEngine;

namespace Features.Network.Client
{
    public class ClientMessageSender : MonoBehaviour
    {
        [SerializeField] private float sendFrequency;
        
        private MovementMessage _movementMessage;
        private DarkRiftClient _client;
        public ulong countSend;
        public void Awake()
        {
            countSend = 0;
            _client = ConnectionManager.Instance.Client;
            _movementMessage = new MovementMessage();
        }

        public void Start()
        {
            StartCoroutine(Send());
        }

        private IEnumerator Send()
        {
            while (_client.ConnectionState == ConnectionState.Connected)
            {
                SendMessage();
                countSend++;
                yield return new WaitForSeconds(1 / sendFrequency);
            }
        }

        private void SendMessage()
        {
            using (var writer = DarkRiftWriter.Create())
            {
                _movementMessage.WriteMessage(writer);
                using (var message = Message.Create((ushort)Tags.SendClientMessage, writer))
                {
                    _client.SendMessage(message, SendMode.Unreliable);
                }
            }
        }
    }
}