using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelLayout : MonoBehaviour
{
    /**
     * Create the board
     */
    public abstract void CreateBoard(LevelData levelData);

    /**
     * Layouts pieces
     */
    public abstract void Layout(List<PieceController> pieces);

    /**
     * Convert position to coordinates
     */
    public abstract PieceCoordinates PosToCoord(Vector3 position);

    /**
     * Convert coordinates to position
     */
    public abstract Vector3 CoordToPos(PieceCoordinates coord);

    /**
     * Returns position of the closest coordinate
     */
    public abstract Vector3 Normalize(Vector3 position);

    /**
     * Get the board slot at coordinate
     */
    public abstract SlotController GetSlotAt(PieceCoordinates coord);
}
