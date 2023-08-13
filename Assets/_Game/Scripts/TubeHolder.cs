using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeHolder : MonoBehaviour 
{
    public List<TubeCarPoint> tubePoints;    
    public TubeCarPoint GetNullPoint()
    {
        for (int i = 0; i < tubePoints.Count; i++)
        {
            if (tubePoints[i].isFill == false)
            {
                return tubePoints[i];
            }
        }
        return null;
    }
    public TubeCarPoint GetFillPoint()
    {
        for (int i = 0; i < tubePoints.Count; i++)
        {
            if (tubePoints[i].isFill == true)
            {
                return tubePoints[i];
            }
        }
        return null;
    }
}

