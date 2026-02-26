using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    public int currentLevel = 1;
    public TextMeshProUGUI levelText;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        UpdateLevelUI();
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Yeni sahnede LevelText objesini bul
        levelText = GameObject.Find("LevelText")?.GetComponent<TextMeshProUGUI>();
        UpdateLevelUI();
    }

    public void CompleteLevel()
    {
        currentLevel++;
        UpdateLevelUI();
    }

    void UpdateLevelUI()
    {
        if (levelText != null)
            levelText.text = "Level " + currentLevel;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
