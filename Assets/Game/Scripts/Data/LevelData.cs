using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LevelData : ScriptableObject
{
    [Serializable]
    public struct PieceData
    {
        [Tooltip("Identifies the type of piece")]
        public string type;

        [Tooltip("Final coordinates of the piece on the board")]
        public PieceCoordinates coordinates;

        [Tooltip("Initial coordinates of the piece on the board")]
        public PieceCoordinates beginCoordinates;

        [Tooltip("Piece rotation")]
        [Range(0f, 360f)]
        public float rotation;
    }

    public LayoutType layoutType;

    [Tooltip("List of coordinates that make up the board")]
    public PieceCoordinates[] board;

    [Tooltip("List of PieceData that make up the level")]
    public PieceData[] pieces;
}
