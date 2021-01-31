using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectExtensions
{
    public static void DestroyChildren(this Transform transform)
    {
        int j = transform.childCount - 1;
        while (j >= 0)
        {
            GameObject.DestroyImmediate(transform.GetChild(j).gameObject);

            j--;
        }
    }

    public static T FindObject<T>(this Component obj, string name, bool deep = false)
    {
        return obj.transform.FindObject<T>(name, deep);
    }

    public static T FindObject<T>(this Transform transform, string name, bool deep = false)
    {
        Transform t = transform.Find(name);

        if (!t)
            return default(T);

        T c = t.GetComponent<T>();

        if (c == null)
            return default(T);

        return c;
    }

    public static T FindObject<T>(this Behaviour gameObject, string name, bool deep = false)
    {
        if (deep)
        {
            gameObject.GetComponentsInChildren<T>();
        }

        Transform t = gameObject.transform.Find(name);

        if (!t)
            return default(T);

        T c = t.GetComponent<T>();

        if (c == null)
            return default(T);

        return c;
    }
}