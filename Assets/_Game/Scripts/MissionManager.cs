using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class MissionManager : MonoBehaviour
{
    public List<Transform> missionTransforms;

    public Transform currentMissionTransform;
    private Transform lastSelectedMission; // Son seçilen görevi tutan değişken.

    [Header("Arrow")]
    public GameObject Arrow;
    public void Start()
    {
        FindMission();
    }

    public void FindMission()
    {
        do
        {
            int randomMissionIndex = Random.Range(0, missionTransforms.Count);
            currentMissionTransform = missionTransforms[randomMissionIndex];
        }
        while (currentMissionTransform == lastSelectedMission); // Eğer yeni görev, son seçilen görevle aynı ise tekrar seçim yap.

        lastSelectedMission = currentMissionTransform;
        currentMissionTransform.gameObject.SetActive(true);

        //int randomMissionIndex = Random.Range(0, missionTransforms.Count);
        //currentMissionTransform = missionTransforms[randomMissionIndex];
        //currentMissionTransform.gameObject.SetActive(true);
    }
    void LateUpdate()
    {
        if (currentMissionTransform != null)
        {
            Arrow.SetActive(true);
            Arrow.transform.LookAt(currentMissionTransform.position);
        }
        else Arrow.SetActive(false);
    }
    void OnEnable()
    {
        GameEvents.missionComplateEvent.AddListener(MissionComplateEvent);
    }


    void OnDisable()
    {
        GameEvents.missionComplateEvent.RemoveListener(MissionComplateEvent);
    }

    private void MissionComplateEvent()
    {
        FindMission();
    }
}
