using UnityEngine;

public static class LiquidGenerator
{
    private static string[] liquids = { "Red", "Blue", "Green", "Yellow" };
    private static System.Collections.Generic.List<string> pool;
    private static int poolIndex = 0;

    // Returns next liquid color or null when no more prefilled liquids available
    public static string GetNextLiquid()
    {
        if (pool == null)
            InitializePool();

        if (poolIndex >= pool.Count)
            return null;

        return pool[poolIndex++];
    }

    private static void InitializePool()
    {
        pool = new System.Collections.Generic.List<string>();

        // Count all fillable slots in scene (only bottles that are not startEmpty)
        var bottles = Object.FindObjectsOfType<BottleView>();
        int totalSlots = 0;
        foreach (var b in bottles)
        {
            if (!b.startEmpty)
                totalSlots += b.liquidTransforms.Length;
        }

        if (totalSlots <= 0)
            return;

        // Reduce to nearest multiple of 3 so we can make per-color counts multiples of 3
        int filledSlots = totalSlots - (totalSlots % 3);
        int units = filledSlots / 3; // each unit represents 3 items

        if (units <= 0)
            return;

        int colors = liquids.Length;
        int baseUnits = units / colors;
        int remainder = units % colors;

        for (int i = 0; i < colors; i++)
        {
            int unitsForColor = baseUnits + (i < remainder ? 1 : 0);
            int countForColor = unitsForColor * 3;
            for (int k = 0; k < countForColor; k++)
                pool.Add(liquids[i]);
        }

        // Shuffle pool for randomness
        Shuffle(pool);
        poolIndex = 0;
    }

    private static void Shuffle(System.Collections.Generic.List<string> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            var tmp = list[i];
            list[i] = list[j];
            list[j] = tmp;
        }
    }

    public static string GetRandomLiquid()
    {
        // Backwards-compatible: return truly random color (not used for initial fill anymore)
        int index = Random.Range(0, liquids.Length);
        return liquids[index];
    }

    public static void ResetPool()
    {
        pool = null;
        poolIndex = 0;
    }
}
