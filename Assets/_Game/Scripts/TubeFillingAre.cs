using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TubeFillingAre : MonoBehaviour
{

    public int fillRate = 1;
    
    public GameObject tubePrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(FillCar(other.GetComponent<TubeHolder>()));
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines() ;
        }
    }
    IEnumerator FillCar(TubeHolder tubeHolder)
    {
        yield return new WaitForSeconds(fillRate);
        TubeCarPoint point = tubeHolder.GetNullPoint();
        if (point !=null)
        {
            point.isFill = true;
            var obj = Instantiate(tubePrefab, point.transform.position,Quaternion.identity);
            obj.transform.SetParent(point.transform);
            obj.transform.rotation = point.transform.rotation;
        }
        StartCoroutine(FillCar(tubeHolder));
    }
}
