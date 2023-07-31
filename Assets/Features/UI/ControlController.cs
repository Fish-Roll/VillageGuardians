using UnityEngine;
using UnityEngine.UI;

namespace Features.UI
{
    public class ControlController : MonoBehaviour
    {
        /// <summary>
        /// if true == keyboard
        /// </summary>
        [SerializeField] private bool girlControl;
        [SerializeField] private AudioSource pushSound;
        [SerializeField] private Transform leftSprite;
        [SerializeField] private Transform rightSprite;
        private Vector3 _spritePos = new Vector3();
        
        private bool _boyControl;

        public bool IsGirlKeyboard => girlControl;
        public void Awake()
        {
            if (girlControl)
                _boyControl = false;
            else
                _boyControl = true;
            
            DontDestroyOnLoad(this.gameObject);
        }

        public void ChangeControl()
        {
            pushSound.Play();
            girlControl = !girlControl;
            _boyControl = !_boyControl;
            
            _spritePos = leftSprite.position;
            leftSprite.position = rightSprite.position;
            rightSprite.position = _spritePos;
        }
    }
}