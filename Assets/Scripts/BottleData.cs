using System;

public class BottleData
{
    private Stack liquids = new Stack();
    private int capacity;

    public BottleData(int capacity)
    {
        this.capacity = capacity;
    }

    public int Capacity => capacity;

    public int Count()
    {
        return liquids.Count();
    }

    public bool IsEmpty()
    {
        return liquids.IsEmpty();
    }

    public bool IsFull()
    {
        return liquids.IsFull();
    }

    public void AddLiquid(string liquid)
    {
        if (!IsFull())
            liquids.Push(liquid);
    }

    public string Peek()
    {
        return liquids.Peek();
    }

    public bool CanPourInto(BottleData otherBottle)
    {
        if (IsEmpty())
            return false;

        if (otherBottle.IsFull())
            return false;

        if (otherBottle.IsEmpty())
            return true;

        return otherBottle.Peek() == Peek();
    }

    public void PourInto(BottleData otherBottle)
    {
        if (!CanPourInto(otherBottle))
            return;

        string liquid = liquids.Pop();
        otherBottle.AddLiquid(liquid);
    }

    // Görsel için güvenli kopya
    public Stack GetClonedStack()
    {
        return liquids.Clone();
    }
    public bool IsComplete()
    {
        if (IsEmpty())
            return true;

        if (Count() != Capacity)
            return false;

        string first = Peek();

        Stack temp = GetClonedStack();

        while (temp.Count() > 0)
        {
            if (temp.Pop() != first)
                return false;
        }

        return true;
    }

}
