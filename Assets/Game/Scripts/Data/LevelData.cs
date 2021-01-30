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

        [Range(0f, 360f)]
        public float rotation;
    }

    public PieceData[] pieces;
}
