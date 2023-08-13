using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image blackPanel;
    void OnEnable()
    {
        GameEvents.endGameEvent += GetRemainingTimeEvent;
    }


    void OnDisable()
    {
        GameEvents.endGameEvent -= GetRemainingTimeEvent;
    }
    private void GetRemainingTimeEvent()
    {
        blackPanel.DOColor(Color.black, 1f).OnComplete(() =>
        {
            // sahne degis
            SceneManager.LoadScene("Menu");
        });
        Debug.Log("Oyun Bitti");
    }
}
