using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHeavyAttackView : MonoBehaviour
{
    [SerializeField] private GameObject innerRing;
    [SerializeField] private GameObject outerRing;
    [SerializeField] private float maxValue;
    [SerializeField] private float time;

    private void OnEnable()
    {
        StartCoroutine(Move());
    }

    private void OnDisable()
    {
        innerRing.transform.localScale = new Vector3(150, innerRing.transform.localScale.y, 150);
        outerRing.transform.localScale = new Vector3(150, innerRing.transform.localScale.y, 150);
    }

    private IEnumerator Move()
    {
        Vector3 currScale = innerRing.transform.localScale;
        Vector3 goalScale = new Vector3(maxValue, innerRing.transform.localScale.y, maxValue);
        
        
        float timePassed = 0.0f;
        float fracTime = 0.0f;
        while (fracTime < 1)
        {
            innerRing.transform.localScale = Vector3.Lerp(currScale, goalScale, fracTime);
            outerRing.transform.localScale = Vector3.Lerp(currScale, goalScale, fracTime);
            timePassed += Time.deltaTime;
            fracTime = timePassed / time;
            yield return null;
        }

        innerRing.transform.localScale = goalScale;
        outerRing.transform.localScale = goalScale;
    }
}
