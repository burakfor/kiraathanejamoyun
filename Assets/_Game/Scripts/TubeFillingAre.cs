using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TubeFillingAre : MonoBehaviour
{

    public int fillRate = 1;
    
    public GameObject tubePrefab;

    public Canvas canvas;
    public Image barImage;
    public TextMeshProUGUI maxTubeText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FillCar(other.GetComponent<TubeHolder>()));
            canvas.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines() ;
            canvas.gameObject.SetActive(false);
            maxTubeText.gameObject.SetActive(false);
        }
    }
    IEnumerator FillCar(TubeHolder tubeHolder)
    {
        barImage.DOFillAmount(1, 1f);
        
        yield return new WaitForSeconds(fillRate);

        TubeCarPoint point = tubeHolder.GetNullPoint();

        if (point !=null)  // boş nokta varsa
        {
            barImage.fillAmount = 0;
            point.isFill = true;
            var obj = Instantiate(tubePrefab, point.transform.position,Quaternion.identity);
            obj.transform.SetParent(point.transform);
            obj.transform.rotation = point.transform.rotation;
        }
        else
        {
            maxTubeText.gameObject.SetActive(true);
        }
        StartCoroutine(FillCar(tubeHolder));
    }
}
