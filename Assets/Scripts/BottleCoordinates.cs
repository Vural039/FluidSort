using UnityEngine;

public class BottleCoordinates
{
    private Vector2[] coordinates = new Vector2[]
{
    new Vector2(-2,3),
    new Vector2(0,3),
    new Vector2(2,3),
    new Vector2(-2,-1),
    new Vector2(0,-1),
    new Vector2(2,-1)
};
    private bool[] isOccupied = new bool[6];

    public Vector2 GetCoordinates(int index)
    {
        if (index < 0 || index >= coordinates.Length)
        {
            Debug.LogError("Index out of range.");
            return Vector2.zero; // Return a default value if the index is out of range
        }
        if (isOccupied[index])
        {
            Debug.LogError("Coordinates at index " + index + " are already occupied.");
            return Vector2.zero; // Return a default value if the coordinates are already occupied
        }
        isOccupied[index] = true;
        return coordinates[index];
    }

    public Vector2 getRandomCoordinates()
    {
        int randomIndex = Random.Range(0, coordinates.Length);
        while (isOccupied[randomIndex])
        {
            randomIndex = Random.Range(0, coordinates.Length);
        }
        isOccupied[randomIndex] = true;
        return coordinates[randomIndex];
    }

    public Vector2 getEmptyCoordinates()
    {
        for (int i = 0; i < isOccupied.Length; i++)
        {
            if (!isOccupied[i])
            {
                isOccupied[i] = true;
                return coordinates[i];
            }
        }
        Debug.LogError("No empty coordinates available.");
        return Vector2.zero; // Return a default value if no empty coordinates are available
    }
    public Vector2 getRandomCoordinateFromEmpty(){
        int randomIndex = Random.Range(0, coordinates.Length);
        while (!isOccupied[randomIndex])
        {
            randomIndex = Random.Range(0, coordinates.Length);
        }
        isOccupied[randomIndex] = true;
        return coordinates[randomIndex];
    }


}
