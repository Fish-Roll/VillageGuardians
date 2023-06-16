using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Features.PickingUp
{
    public class Paper : MonoBehaviour, ILifted
    {
        [SerializeField] private List<GameObject> paperWindow;
        [SerializeField] private string textNote;
        [SerializeField] private List<Text> text;
        [SerializeField] private AudioSource getPaperSound;
        public void Lift()
        {
        }

        public void Lift(GameObject gm)
        {
            getPaperSound.Play();
            if (gm.name == "KeyboardBoy" || gm.name == "GamepadBoy")
            {
                text[0].text = textNote;
                paperWindow[0].SetActive(true);
            }
            else if (gm.name == "KeyboardGirl" || gm.name == "GamepadGirl")
            {
                text[1].text = textNote;
                paperWindow[1].SetActive(true);
            }
            Destroy(gameObject, 0.5f);

        }
    }
}