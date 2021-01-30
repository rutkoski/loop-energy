using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelLayout : MonoBehaviour
{
    public abstract void Layout(List<PieceController> pieces);

    public abstract PieceCoodinates PosToCoord(Vector3 position);

    public abstract Vector3 CoordToPos(PieceCoodinates coord);

    public abstract Vector3 Normalize(Vector3 position);
}
