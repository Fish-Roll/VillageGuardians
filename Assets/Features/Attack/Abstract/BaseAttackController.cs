using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Features.Attack.Abstract
{
    public abstract class BaseAttackController : MonoBehaviour
    {
        [SerializeField] protected List<BaseAttack> attacks;
        [SerializeField] protected Animator animator;
        public static bool canAttack;
        public abstract void HandleAttack(int type);
    }
}