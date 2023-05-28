using System.Collections;
using UnityEngine;

namespace Features.PickingUp
{
    public class Paper : MonoBehaviour, ILifted
    {
        [SerializeField] private GameObject paperWindow;
        
        public void Lift()
        {
            paperWindow.SetActive(true);
            Destroy(gameObject);
        }
    }
}