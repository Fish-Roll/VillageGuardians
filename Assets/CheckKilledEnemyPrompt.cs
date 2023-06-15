using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckKilledEnemyPrompt : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private List<GameObject> enemies;
    [SerializeField] private Collider promptCollider;
    private void Start()
    {
        promptCollider.enabled = false;
        StartCoroutine(CheckEnemyCount());
    }
    
    private int countNull;
    
    private IEnumerator CheckEnemyCount()
    {
        while (true)
        {
            countNull = 0;
            for (int i = 0; i < enemies.Count; i++)
            {
                if (enemies[i] == null)
                    countNull++;
            }

            if (countNull == enemies.Count)
            {
                wall.tag = "Wall";
                promptCollider.enabled = true;
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
