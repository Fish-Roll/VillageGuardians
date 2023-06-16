using System;
using System.Collections;
using System.Collections.Generic;
using Features.Health;
using UnityEngine;

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private GameObject bgDeath;
    [SerializeField] private List<GameObject> checkpoints;
    [SerializeField] private List<GameObject> spawnPoints;
    
    public GameObject[] players;
    public GameObject currentSpawnPoint;
    public PlayerHealthController[] controllers;
    private void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
    }

    public void Respawn()
    {
        Vector3 newPoint = currentSpawnPoint.transform.position;
        newPoint.z += 2;
        players[0].transform.position = newPoint;
        players[0].GetComponent<PlayerHealthController>().Revive();
        
        newPoint = currentSpawnPoint.transform.position;
        newPoint.z -= 2;
        players[1].transform.position = newPoint;
        players[1].GetComponent<PlayerHealthController>().Revive();
        bgDeath.SetActive(false);
    }
}
