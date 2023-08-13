using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Animator animator;
    public Image LoadingBarImage;
    public void StarGame(string levelName)
    {
        animator.SetBool("isOpen", true);
        StartCoroutine(LoadSceneAsync(levelName));
    }
    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    IEnumerator LoadSceneAsync(string levelName)
    {
        yield return new WaitForSeconds(2f);
        AsyncOperation operation = SceneManager.LoadSceneAsync(levelName);
        while (!operation.isDone)
        {
            float value = Mathf.Clamp01(operation.progress / 0.9f);
            LoadingBarImage.fillAmount = value;
            yield return null;
        }
    }
}
