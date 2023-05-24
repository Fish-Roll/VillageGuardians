using System;
using Features.Attack.Abstract;

namespace Features.Attack.Girl
{
    public class GirlAttackController : BaseAttackController
    {
        private void Start()
        {
            for(int i = 0; i < attacks.Count; i++)
                attacks[i].Init(animator);
        }

        public override void HandleAttack(int type)
        {
            StartCoroutine(attacks[type].Attack());
        }
    }
}