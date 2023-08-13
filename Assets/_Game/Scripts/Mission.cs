using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    public Canvas canvas;
    public Image processBarImage;
    public TextMeshProUGUI tubeNoText;

    public float timer;
    public Transform CustomerTransform;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StartCoroutine(IncreaseTimer(other.GetComponent<TubeHolder>()));
            canvas.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            StopAllCoroutines();
            timer = 0;
            canvas.gameObject.SetActive(false);
            tubeNoText.gameObject.SetActive(false);
        }
    }
    IEnumerator IncreaseTimer(TubeHolder tubeHolder)
    {
        yield return new WaitForSeconds(1f);
        timer += 1;
        processBarImage.fillAmount = timer / 5;
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
            else
            {
                tubeNoText.gameObject.SetActive(true);
            }
        }
        else StartCoroutine(IncreaseTimer(tubeHolder));
    }
}
