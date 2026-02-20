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
        LevelCounter levelCounter = FindObjectOfType<LevelCounter>();
        if (levelCounter != null) levelCounter.IncreaseLevel();
        gameObject.SetActive(true);
    }

    // public void LoadNextLevel()
    // {
    //     Scene currentScene = SceneManager.GetActiveScene();
    //     SceneManager.LoadScene(currentScene.name);
    // }
}
