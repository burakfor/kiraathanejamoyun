using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public float timer;
    public Transform CustomerTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(IncreaseTimer(other.GetComponent<TubeHolder>()));
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
    IEnumerator IncreaseTimer(TubeHolder tubeHolder)
    {
        yield return new WaitForSeconds(1f);
        timer += 1;
        if (timer > 5)
        {
            TubeCarPoint point = tubeHolder.GetFillPoint();
            if (point != null) // arabada tüp varsa
            {
                point.isFill = false;
                var tube = point.transform.GetChild(0);
                tube.DOMove(CustomerTransform.position, 1f);
                Destroy(tube.gameObject,3f);
                tube.SetParent(null);
                gameObject.SetActive(false);
                GameEvents.missionComplateEvent?.Invoke();
            }
        }
        else StartCoroutine(IncreaseTimer(tubeHolder));
    }
}
