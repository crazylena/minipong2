using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public string gameSceneName = "Game";
    public float loadTime = 1.2f;

    private void Start()
    {
        Invoke(nameof(LoadGameScene), loadTime);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(gameSceneName);
    }
}