using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor; // optional in editor-only scripts

public class GameChecker : MonoBehaviour
{
    public bool isFinished=false;
    public bool isSuccess=false;
    public int numberOfBottles;

    void Start()
    {
        numberOfBottles = CountBottlesSceneOnly();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGameState();
        if (isFinished)
        {
            if (isSuccess)
            {
                Debug.Log("Congratulations! You've completed the level!");
                // Optionally, load next level or show success UI
            }
            else
            {
                Debug.Log("Level failed. Try again!");
                // Optionally, reload current level or show failure UI
            }
        }
    }

    int CountBottlesSceneOnly()//şişe sayısını say
    {
        return Resources.FindObjectsOfTypeAll<GameObject>()
            .Count(go => go.name == "Bottle" && go.scene.IsValid());
    }

    public void CheckGameState()
    {
        int emptyCount = 0;
        int fullCount = 0;

        foreach (var bottle in Resources.FindObjectsOfTypeAll<GameObject>()
            .Where(go => go.name == "Bottle" && go.scene.IsValid()))
        {
            var bottleView = bottle.GetComponent<BottleView>();
            if (bottleView != null)
            {
                if (bottleView.GetBottleData().IsEmpty())
                    emptyCount++;
                else if (bottleView.GetBottleData().IsFull())
                    fullCount++;
            }
        }

        isFinished = (emptyCount + fullCount) == numberOfBottles;
        isSuccess = fullCount == numberOfBottles;
    }
}
