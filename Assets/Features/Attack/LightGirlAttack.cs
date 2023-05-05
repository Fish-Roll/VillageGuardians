using System.Collections;
using Features.Attack.Weapon;
using UnityEngine;

namespace Features.Attack
{
    public class LightGirlAttack : MonoBehaviour
    {
        [SerializeField] private Transform spawnAttack;
        [SerializeField] private GameObject fireball;
        [SerializeField] private float cooldownDuration;
        [SerializeField] private float lightAttackDelay;

        private bool _canAttack;
        private bool _alreadyAttack;
        public void Init(ref bool canAttack)
        {
            _canAttack = canAttack;
        }

        public IEnumerator Attack(Vector3 mousePosition)
        {
            yield return new WaitForSeconds(lightAttackDelay);
            _canAttack = false;
            Vector3 rotate = (mousePosition - spawnAttack.position).normalized;
            Instantiate(fireball, spawnAttack.position, Quaternion.LookRotation(rotate, Vector3.up));
            //obj.GetComponent<Fireball>().Move(rotate);
            StartCoroutine(ResetAttack());
        }

        //поворачивать перса вместе с спавнером, тогда получится убрать прицел
        private IEnumerator ResetAttack()
        {
            yield return new WaitForSeconds(cooldownDuration);
            _canAttack = true;
            _alreadyAttack = false;
        }
    }
}