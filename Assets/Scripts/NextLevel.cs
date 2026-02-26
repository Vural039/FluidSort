using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowNextLevel()
    {
        gameObject.SetActive(true);
    }

    public void RestartLevel()
    {
        LevelManager.Instance.CompleteLevel();
        LiquidGenerator.ResetPool();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    // public void LoadNextLevel()
    // {
    //     Scene currentScene = SceneManager.GetActiveScene();
    //     SceneManager.LoadScene(currentScene.name);
    // }
}
