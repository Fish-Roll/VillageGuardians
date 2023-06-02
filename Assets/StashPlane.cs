using Features;
using UnityEngine;

public class StashPlane : MonoBehaviour
{
    [SerializeField] private Gate[] gates;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gates[0].enabled = true;
            gates[1].enabled = true;
        }
    }
}