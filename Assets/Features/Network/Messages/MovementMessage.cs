using DarkRift;
using UnityEngine;

namespace Features.Network.Messages
{
    public class MovementMessage
    {
        private Vector3 _position;
        private Vector3 _velocity;
        private Quaternion _rotation;

        public void WriteMessage(DarkRiftWriter writer)
        {
            MessageSerializer.WriteVector3(writer, _position);
            MessageSerializer.WriteVector3(writer, _velocity);
            MessageSerializer.WriteQuaternion(writer, _rotation);
        }
    }
}