using Features;
using UnityEngine;

public class StashPlane : MonoBehaviour
{
    [SerializeField] private Gate[] gates;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gates[0].Open();
            gates[1].Open();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gates[0].Close();
            gates[1].Close();
        }
    }
}