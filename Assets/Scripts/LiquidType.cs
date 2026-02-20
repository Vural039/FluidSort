public class LiquidType
{
    public string[] liquids = new string[5] { "Red", "Blue", "Green", "Yellow", null };

    public string GetRandomLiquid()
    {
        int randomIndex = UnityEngine.Random.Range(0, liquids.Length);
        return liquids[randomIndex];
    }
    
}
