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
        public string type;

        public PieceCoordinates coordinates;

        public PieceCoordinates beginCoordinates;

        [Range(0f, 360f)]
        public float rotation;
    }

    public LayoutType layoutType;

    public PieceCoordinates[] board;

    public PieceData[] pieces;
}
