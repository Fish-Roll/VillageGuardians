using UnityEngine;

namespace Features.Health.Abstract
{
    public abstract class EnemyBaseHealthController : MonoBehaviour
    {
        public abstract void Damage(float value);
    }
}