using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public float timer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(IncreaseTimer());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StopAllCoroutines();
            timer = 0;
        }
    }
    IEnumerator IncreaseTimer()
    {
        yield return new WaitForSeconds(1f);
        timer += 1;
        if (timer > 5)
        {
            GameEvents.missionComplateEvent?.Invoke();
        }
        else StartCoroutine(IncreaseTimer());
    }
}
