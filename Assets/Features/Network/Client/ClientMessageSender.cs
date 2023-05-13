using System;
using System.Collections;
using DarkRift;
using DarkRift.Client;
using Features.Network.Messages;
using UnityEngine;

namespace Features.Network.Client
{
    public class ClientMessageSender : MonoBehaviour
    {
        [SerializeField] private float sendFrequency;
        
        private MovementMessage _movementMessage;
        private DarkRiftClient _client;
        public void Awake()
        {
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
                yield return new WaitForSeconds(1 / sendFrequency);
            }
        }

        private void SendMessage()
        {
            
        }
    }
}