using System;
using System.Collections;
using Features.Interaction;
using Features.PickingUp;
using UnityEngine;

namespace Features.TagsGame
{
    public class Knuckle : MonoBehaviour, ILifted, IInteractable
    {
        [SerializeField] private ushort id;
        [SerializeField] private float time;
        [SerializeField] private GameObject particle;
        [SerializeField] private AudioSource takeKnuckleSound;
        public int Id => id;
        
        private Action<Knuckle> _onInteract;
        private Action<GameObject> _onLift;
        public bool lifted;
        public GameObject Particle => particle; 

        public void InitInteract(Action<Knuckle> onInteract)
        {
            _onInteract = onInteract;
        }
        public void InitLift(Action<GameObject> onLift)
        {
            _onLift = onLift;
        }
        
        /// <summary>
        /// Move cube to game pole
        /// </summary>
        public void Lift()
        {
            if (!lifted)
            {
                takeKnuckleSound.Play();
                lifted = true;
                particle.SetActive(false);
                _onLift.Invoke(gameObject);
            }
        }

        public void Lift(GameObject gm)
        {
            
        }

        public void Interact()
        {
            _onInteract.Invoke(this);
        }

        public IEnumerator Move(TagsPoint targetPoint)
        {
            var targetPosition = targetPoint.transform;
            targetPoint.Knuckle = this;
            
            float currTime = 0;
            while (Vector3.Distance(transform.position, targetPosition.position) >= 0.05f)
            {
                transform.position = Vector3.Lerp(transform.position, targetPosition.position, currTime / time);
                currTime += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPosition.position;
        }
    }
}