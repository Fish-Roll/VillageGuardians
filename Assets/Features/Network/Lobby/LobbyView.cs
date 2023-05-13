using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Features.Network.Lobby
{
    [Serializable]
    public class LobbyView
    {
        [SerializeField] private Button createLobby;
        [SerializeField] private Button leaveLobby;
        [SerializeField] private Button joinLobby;
        
        private LobbyInfo _lobby;

        public LobbyInfo Lobby
        {
            get => _lobby;
            set => _lobby = value;
        }

        public void Init(UnityAction onCreate, UnityAction onLeave, UnityAction onJoin)
        {
            createLobby.onClick.AddListener(onCreate);
            leaveLobby.onClick.AddListener(onLeave);
            joinLobby.onClick.AddListener(onJoin);
        }
        
    }
}