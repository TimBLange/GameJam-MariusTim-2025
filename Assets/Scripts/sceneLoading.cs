using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class sceneLoading : MonoBehaviour
{
    [SerializeField] GameObject LoadingScreen;
    [SerializeField] Image LoadingBarFill;
    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        if (LoadingScreen != null)
        {


            LoadingScreen.SetActive(true);
            while (!operation.isDone)
            {
                float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
                LoadingBarFill.fillAmount = progressValue;
                yield return null;
            }
        }
    }
}
