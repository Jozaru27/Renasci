using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    int sceneToLoad;

    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadMainMenuAsync()
    {
        sceneToLoad = 0;
        GoToLoadingScene();
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadNextSceneAsync()
    {
        sceneToLoad = SceneManager.GetActiveScene().buildIndex + 1;
        GoToLoadingScene();
    }

    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void LoadPreviousSceneAsync()
    {
        sceneToLoad = SceneManager.GetActiveScene().buildIndex - 1;
        GoToLoadingScene();
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadCurrentSceneAsync()
    {
        sceneToLoad = SceneManager.GetActiveScene().buildIndex;
        GoToLoadingScene();
    }

    void GoToLoadingScene()
    {
        SceneManager.LoadScene(3);
    }

    public void StartLoading()
    {
        SceneManager.LoadSceneAsync(sceneToLoad);
    }
}
