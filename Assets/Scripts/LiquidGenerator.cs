using UnityEngine;

public static class LiquidGenerator
{
    private static string[] liquids = { "Red", "Blue", "Green", "Yellow" };

    public static string GetRandomLiquid()
    {
        int index = Random.Range(0, liquids.Length);
        return liquids[index];
    }
}
