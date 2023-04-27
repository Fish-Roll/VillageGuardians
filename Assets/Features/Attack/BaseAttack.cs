using System;
using UnityEngine;

namespace Features.Attack
{
    public interface BaseAttack
    {
        public GameObject AttackCollider { get; }
        public void Attack();
    }
}