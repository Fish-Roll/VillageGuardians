using Features.Input;
using UnityEngine;

namespace Features.Movement
{
    public class PlayerRotator
    {
        public void RotatePlayer(Quaternion playerRotation, Vector3 moveDirection, float currentFalling)
        {
            Vector3 positionToLookAt = new Vector3(moveDirection.x, currentFalling, moveDirection.z);
            Quaternion curRotation = playerRotation;
            //if()
        }
    }
}