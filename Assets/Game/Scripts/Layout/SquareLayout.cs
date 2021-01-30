using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareLayout : LevelLayout
{
    [SerializeField] private Vector3 m_gridSize;

    public override Vector3 CoordToPos(PieceCoodinates coord)
    {
        Vector3 pos = new Vector3();

        pos.x = coord.x * m_gridSize.x;
        pos.y = coord.y * m_gridSize.y;
        pos.z = 0;

        return pos;
    }

    public override PieceCoodinates PosToCoord(Vector3 position)
    {
        PieceCoodinates coord = new PieceCoodinates();
        coord.x = (int)Mathf.Round(position.x / m_gridSize.x);
        coord.y = (int)Mathf.Round(position.y / m_gridSize.y);
        coord.z = 0;

        return coord;
    }

    public override Vector3 Normalize(Vector3 position)
    {
        return CoordToPos(PosToCoord(position));
    }

    public override void Layout(List<PieceController> pieces)
    {
        foreach (PieceController piece in pieces)
        {
            piece.transform.position = CoordToPos(piece.Coordinates);
            Debug.Log(piece.Coordinates.x + " " + piece.Coordinates.y + " " + piece.transform.position);
        }
    }
}
