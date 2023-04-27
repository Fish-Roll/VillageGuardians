using DarkRift;
using UnityEngine;

namespace Features.Network
{
    public static class MessageSerializer
    {
        public static void WriteVector3(DarkRiftWriter writer, Vector3 vector)
        {
            writer.Write(vector.x);
            writer.Write(vector.y);
            writer.Write(vector.z);
        }

        public static void WriteQuaternion(DarkRiftWriter writer, Quaternion quaternion)
        {
            writer.Write(quaternion.x);
            writer.Write(quaternion.y);
            writer.Write(quaternion.z);
        }
        
    }
}