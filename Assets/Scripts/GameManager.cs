using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI (optional)")]
    public GameObject winPanel;
    public GameObject losePanel;

    private BottleView selectedBottle;
    private BottleView[] allBottles;

    private bool isGameOver = false;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        allBottles = FindObjectsOfType<BottleView>();
        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
        if (isGameOver) return;

        Vector2 worldPoint;

        // Editör (Mouse)
        if (Input.GetMouseButtonDown(0))
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            TrySelectBottle(worldPoint);
        }

        // Android (Touch)
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            worldPoint = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            TrySelectBottle(worldPoint);
        }
    }

    void TrySelectBottle(Vector2 worldPoint)
    {
        Collider2D hit = Physics2D.OverlapPoint(worldPoint);

        if (hit == null)
            return;

        BottleView bottle = hit.GetComponentInParent<BottleView>();

        if (bottle == null)
            return;

        SelectBottle(bottle);
    }

    void SelectBottle(BottleView bottle)
    {
        if (selectedBottle == null)
        {
            selectedBottle = bottle;
            bottle.SetSelected(true);
        }
        else
        {
            if (selectedBottle == bottle)
            {
                selectedBottle.SetSelected(false);
                selectedBottle = null;
                return;
            }

            selectedBottle.PourInto(bottle);

            selectedBottle.SetSelected(false);
            selectedBottle = null;

            CheckWinCondition();
            CheckLoseCondition();
        }
    }

    void CheckWinCondition()
    {
        foreach (BottleView bottle in allBottles)
        {
            if (!bottle.IsComplete())
                return;
        }
        Win();
    }

    void CheckLoseCondition()
    {
        // If any valid pour exists, game is not lost
        for (int i = 0; i < allBottles.Length; i++)
        {
            for (int j = 0; j < allBottles.Length; j++)
            {
                if (i == j) continue;

                var a = allBottles[i];
                var b = allBottles[j];

                if (a == null || b == null) continue;

                var aData = a.GetBottleData();
                var bData = b.GetBottleData();

                if (aData.CanPourInto(bData))
                    return; // move exists
            }
        }

        // No moves and not all complete => lose
        Lose();
    }

    void Win()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("LEVEL COMPLETED - WIN");
        NextLevel nextLevelButton = FindObjectOfType<NextLevel>(true);
        if (nextLevelButton != null) nextLevelButton.ShowNextLevel();
        if (winPanel != null) winPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    void Lose()
    {
        if (isGameOver) return;
        isGameOver = true;
        Debug.Log("NO MOVES LEFT - LOSE");
        if (losePanel != null) losePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // UI buttons can call these
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
    }
}
