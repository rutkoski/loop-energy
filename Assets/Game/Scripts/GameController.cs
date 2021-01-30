using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Transform m_board;

    private List<PieceController> m_pieces = new List<PieceController>();

    private List<PieceController> m_targets = new List<PieceController>();

    private void Start()
    {
        //EndGame();
        StartGame();
    }

    private void StartGame()
    {
        Debug.Log("[GameController] Game started");

        m_pieces.Clear();
        m_pieces.AddRange(m_board.GetComponentsInChildren<PieceController>());

        m_targets.Clear();

        foreach (PieceController piece in m_pieces)
        {
            piece.Interactable = true;

            if (piece.IsTarget)
            {
                m_targets.Add(piece);
            }
        }

        PieceController.OnStateChanged += PieceController_OnChange;
    }

    private void EndGame()
    {
        Debug.Log("[GameController] Game ended");

        PieceController.OnStateChanged -= PieceController_OnChange;

        foreach (PieceController piece in m_pieces)
        {
            piece.Interactable = false;
        }
    }

    private void PieceController_OnChange(object sender, System.EventArgs args)
    {
        Physics2D.SyncTransforms();

        List<PieceController> visited = new List<PieceController>();

        foreach (PieceController piece in m_pieces)
        {
            piece.UpdateConnections();
        }

        foreach (PieceController piece in m_pieces)
        {
            if (piece.IsSource)
            {
                piece.SetConnectedToSource(visited);
            }
        }

        if (CheckWin())
        {
            EndGame();
        }
    }

    private bool CheckWin()
    {
        bool connected = true;

        foreach (PieceController piece in m_targets)
        {
            connected &= piece.ConnectedToSource;
        }

        return connected;
    }
}
