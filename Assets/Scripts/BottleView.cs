using UnityEngine;

public class BottleView : MonoBehaviour
{
    public Transform[] liquidTransforms;
public bool startEmpty = false;

    private BottleData data;

    void Start()
{
    data = new BottleData(liquidTransforms.Length);

    if (!startEmpty)
    {
        for (int i = 0; i < liquidTransforms.Length; i++)
        {
            string liquid = LiquidGenerator.GetNextLiquid();
            if (!string.IsNullOrEmpty(liquid))
                data.AddLiquid(liquid);
            else
                break; // pool exhausted — leave remaining slots empty
        }
    }

    UpdateVisuals();
}


    public void UpdateVisuals()
    {
        Stack tempStack = data.GetClonedStack();
        int count = tempStack.Count();

        // Render from bottom to top (index 0 at bottom, higher indices at top)
        for (int i = 0; i < liquidTransforms.Length; i++)
        {
            // Only fill from bottom up based on count
            if (i < count)
            {
                string liquid = tempStack.GetAtIndex(i);
                liquidTransforms[i]
                    .GetComponent<Renderer>()
                    .material.color = GetColorFromLiquid(liquid);
            }
            else
            {
                // Top parts invisible when not filled
                liquidTransforms[i]
                    .GetComponent<Renderer>()
                    .material.color = Color.clear;
            }
        }
    }

    public void PourInto(BottleView otherView)
    {
        data.PourInto(otherView.data);

        UpdateVisuals();
        otherView.UpdateVisuals();
    }

    private Color GetColorFromLiquid(string liquid)
    {
        switch (liquid)
        {
            case "Red": return Color.red;
            case "Blue": return Color.blue;
            case "Green": return Color.green;
            case "Yellow": return Color.yellow;
            default: return Color.clear;
        }
    }

    public void SetSelected(bool value)
    {
        if (value)
            transform.localScale = Vector3.one * 1.1f;
        else
            transform.localScale = Vector3.one;
    }

    public bool IsComplete()
    {
        return data.IsComplete();
    }

    public BottleData GetBottleData()
    {
        return data;
    }

}
