using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Checkpoints checkpoints;
    [SerializeField] private GameObject spawnPoint;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            checkpoints.currentSpawnPoint = spawnPoint;
            Destroy(gameObject, 0.2f);
        }
    }
}
