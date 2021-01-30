using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PieceCoordinates
{
    public int x = 0;
    public int y = 0;
    public int z = 0;

    public void Set(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public void Set(PieceCoordinates coord)
    {
        this.x = coord.x;
        this.y = coord.y;
        this.z = coord.z;
    }

    public static bool operator ==(PieceCoordinates coord1, PieceCoordinates coord2)
    {
        return coord1.x == coord2.x && coord1.y == coord2.y && coord1.z == coord2.z;
    }

    public static bool operator !=(PieceCoordinates coord1, PieceCoordinates coord2)
    {
        return coord1.x != coord2.x || coord1.y != coord2.y || coord1.z != coord2.z;
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
