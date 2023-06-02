using System;
using UnityEngine;

namespace Features.TagsGame
{
    public class TagsPoint : MonoBehaviour
    {
        [SerializeField] private int id;
        private Vector3 _position;
        
        private void Start()
        {
            _position = transform.position;
        }

    }
}