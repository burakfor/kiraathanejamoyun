
using System;
using System.Collections.Generic;
using UnityEngine;

public class PedestrianPath : MonoBehaviour
{
    public List<Transform> paths;
    public Color lineColor;
    public GameObject pedestrianPrefab;
    public int spawnRate;
    public int maxCount;

    private void OnValidate()
    {
        for (int i = 0; i < transform.childCount; i++) 
        {
            if (!paths.Contains(transform.GetChild(i).transform))
            {
                paths.Add(transform.GetChild (i).transform);
            }
        }
    }
    public Transform GetCurrentPathTransform(int currentPathIndex)
    {
        return paths[currentPathIndex];
    }
    private void Start()
    {
        InvokeRepeating(nameof(SpawnPedestrian), spawnRate, spawnRate);
    }
    private int GetCurrentPedestrianCount()
    {
        return FindObjectsOfType<Pedestrian>().Length;
    }
    private void SpawnPedestrian()
    {
        if (GetCurrentPedestrianCount()<= maxCount)
        {
            var obj = Instantiate(pedestrianPrefab);
            int r = UnityEngine.Random.Range(0, paths.Count);
            obj.transform.position = paths[r].position;
            Pedestrian pedestrian = obj.GetComponent<Pedestrian>();
            pedestrian.currentPathIndex = r;
            pedestrian.path = this;
        }

    }

}
