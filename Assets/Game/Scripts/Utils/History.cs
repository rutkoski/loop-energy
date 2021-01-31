using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class History : Singleton<History>
{

    Stack<object> stack = new Stack<object>();
    
    public void Push(object value)
    {
        stack.Push(value);
    }

    public object Back()
    {
        return stack.Count > 0 ? stack.Pop() : null;
    }

    public object Peek()
    {
        return stack.Count > 0 ? stack.Peek() : null;
    }

    public void Clear()
    {
        stack.Clear();
    }
}
