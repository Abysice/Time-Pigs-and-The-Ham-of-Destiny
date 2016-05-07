using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommandStack<T>
{
    private List<T> items = new List<T>();

    public int Count
    {
        get { return items.Count;  }
    }

    public void Push(T item)
    {
        items.Add(item);
    }

    public T Pop()
    {
        if (items.Count > 0)
        {
            T temp = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return temp;
        }
        else
            return default(T);
    }

    public T Peek()
    {
        if (items.Count > 0)
        {
            return items[items.Count - 1];
        }
        else
            return default(T);
    }

    public void RemoveFirstElement()
    {
        items.RemoveAt(0);
    }
}
