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
            data.AddLiquid(LiquidGenerator.GetRandomLiquid());
        }
    }

    UpdateVisuals();
}


    public void UpdateVisuals()
    {
        Stack tempStack = data.GetClonedStack();

        for (int i = 0; i < liquidTransforms.Length; i++)
        {
            if (tempStack.Count() > 0)
            {
                string liquid = tempStack.Pop();
                liquidTransforms[i]
                    .GetComponent<Renderer>()
                    .material.color = GetColorFromLiquid(liquid);
            }
            else
            {
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
