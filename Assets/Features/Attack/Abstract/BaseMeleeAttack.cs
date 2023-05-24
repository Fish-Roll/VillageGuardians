using System.Collections;
using UnityEngine;

namespace Features.Attack.Abstract
{
    public abstract class BaseMeleeAttack : BaseAttack
    {
        [SerializeField] protected Collider weapon;
    }
}