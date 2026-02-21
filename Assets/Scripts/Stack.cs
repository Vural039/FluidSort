using UnityEngine;

public class Stack
{
    string[] stack = new string[3];

    public void Push(string item)
    {
        for (int i = 0; i < stack.Length; i++)
        {
            if (stack[i] == null)
            {
                stack[i] = item;
                return;
            }
        }
        Debug.Log("Stack is full!");
    }

    public string Pop()
    {
        for (int i = stack.Length - 1; i >= 0; i--)
        {
            if (stack[i] != null)
            {
                string item = stack[i];
                stack[i] = null;
                return item;
            }
        }
        Debug.Log("Stack is empty!");
        return null;
    }

    public string Peek()
    {
        for (int i = stack.Length - 1; i >= 0; i--)
        {
            if (stack[i] != null)
            {
                return stack[i];
            }
        }
        Debug.Log("Stack is empty!");
        return null;
    }

    public bool IsEmpty()
    {
        for (int i = 0; i < stack.Length; i++)
        {
            if (stack[i] != null)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsFull()
    {
        for (int i = 0; i < stack.Length; i++)
        {
            if (stack[i] == null)
            {
                return false;
            }
        }
        return true;
    }

    public int Count()
    {
        int count = 0;
        for (int i = 0; i < stack.Length; i++)
        {
            if (stack[i] != null)
            {
                count++;
            }
        }
        return count;
    }

    public Stack Clone()
    {
        Stack newStack = new Stack();
        for (int i = 0; i < stack.Length; i++)
        {
            if (stack[i] != null)
            {
                newStack.Push(stack[i]);
            }
        }
        return newStack;
    }

    public string GetAtIndex(int index)
    {
        if (index >= 0 && index < stack.Length)
            return stack[index];
        return null;
    }
}
