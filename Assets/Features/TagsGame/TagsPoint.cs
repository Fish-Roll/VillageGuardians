using System;
using UnityEngine;

namespace Features.TagsGame
{
    public class TagsPoint : MonoBehaviour
    {
        [SerializeField] private Knuckle _knuckle;
        public Knuckle Knuckle
        {
            get => _knuckle;
            set => _knuckle = value;
        }

        private Vector3 _position;
       
        private void Start()
        {
            _position = transform.position;
        }

    }
}