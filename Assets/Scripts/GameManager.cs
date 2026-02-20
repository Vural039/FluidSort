using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private BottleView selectedBottle;
    private BottleView[] allBottles;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        allBottles = FindObjectsOfType<BottleView>();
    }

    void Update()
    {
        HandleInput();
    }

    void HandleInput()
    {
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
        }
    }

    void CheckWinCondition()
    {
        foreach (BottleView bottle in allBottles)
        {
            if (!bottle.IsComplete())
                return;
        }

        Debug.Log("LEVEL COMPLETED");
    }
}
