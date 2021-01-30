using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PieceCoodinates
{
    public int x;
    public int y;
    public int z;

    public void Set(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void Set(PieceCoodinates coord)
    {
        this.x = coord.x;
        this.y = coord.y;
        this.z = coord.z;
    }
}
