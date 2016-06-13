using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class TransitionManager : Singleton<TransitionManager>
{
    [SerializeField]
    private Image backgroundFiller;
    private float fadeDuration = 0.5f;

    private bool isLoading;
    
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void LoadScene(string sceneName)
    {
        if(isLoading)
        {
            Debug.LogWarning("WARNING: another scene loading is in process!");
            return;
        }

        StartCoroutine(LoadRoutine(sceneName));
    }

    private IEnumerator LoadRoutine(string sceneName)
    {
        isLoading = true;
        backgroundFiller.canvasRenderer.SetAlpha(0);
        backgroundFiller.gameObject.SetActive(true);
        backgroundFiller.CrossFadeAlpha(1, fadeDuration, true);
        yield return new WaitForSeconds(fadeDuration);

        AsyncOperation loading = SceneManager.LoadSceneAsync(sceneName);
        yield return loading;

        backgroundFiller.CrossFadeAlpha(0, fadeDuration, true);
        yield return new WaitForSeconds(fadeDuration);

        backgroundFiller.gameObject.SetActive(false);
        isLoading = false;
    }

}
