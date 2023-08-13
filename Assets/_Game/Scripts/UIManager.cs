using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI RemainingTimeText;
    public string remainingDefaultText;
    public float startRemainingTime;
    public float remainingTime;
    public float addedTime =25;
    public bool isPauseTime;

    void Start()
    {
        remainingTime = startRemainingTime;
    }
    void Update()
    {
        if (!isPauseTime)
        {
            remainingTime -= Time.deltaTime;
            RemainingTimeText.text = remainingDefaultText + Convert.ToInt32(remainingTime).ToString();
        }

        if(remainingTime<=0)
        {
            isPauseTime  = true;
            GameEvents.endGameEvent?.Invoke();
        }
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
        remainingTime += addedTime;
    }
}
