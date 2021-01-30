using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceFactory : MonoBehaviour
{
    [SerializeField] private List<PieceController> m_pieces;

    [SerializeField] private Transform m_board;

    public PieceController Create(string type)
    {
        PieceController prefab = m_pieces.Find(p => p.Type == type);

        PieceController piece = Instantiate(prefab, m_board, false);

        return piece;
    }
}
