using System.Collections;
using UnityEngine;

namespace Features.PickingUp
{
    public interface ILifted
    {
        public void Lift();
        public void Lift(GameObject gm);
    }
}