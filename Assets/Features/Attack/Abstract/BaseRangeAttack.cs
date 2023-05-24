using System.Collections;
using UnityEngine;

namespace Features.Attack.Abstract
{
    public abstract class BaseRangeAttack : BaseAttack
    {
        [SerializeField] protected GameObject projectile;
        [SerializeField] protected Transform spawnPosition;
    }
}