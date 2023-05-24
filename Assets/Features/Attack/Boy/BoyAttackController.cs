using System;
using Features.Attack.Abstract;
using UnityEngine;

namespace Features.Attack.Boy
{
    public class BoyAttackController : BaseAttackController
    {
        private void Start()
        {
            for (int i = 0; i < attacks.Count; i++)
                attacks[i].Init(animator);
        }

        public override void HandleAttack(int type)
        {
            StartCoroutine(attacks[type].Attack());
        }
    }
}