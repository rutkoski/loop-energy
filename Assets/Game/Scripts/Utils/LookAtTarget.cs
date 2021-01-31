using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LookAtTarget : MonoBehaviour
{
    public Transform target;

    public bool editorOnly;

    private void Update()
    {
        if (editorOnly)
        {
#if UNITY_EDITOR
            transform.LookAt(target);
#endif
        }
        else
        {
            transform.LookAt(target);
        }
    }
}
