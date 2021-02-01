using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieceFactory))]
public class GameController : MonoBehaviour
{
    public delegate void GameEndedEvent(object sender, EventArgs args);

    public event GameEndedEvent OnGameEnded;

    private static GameController m_instance;
    public static GameController Instance => m_instance;

    [SerializeField] private Transform m_gameContainer;
    public Transform Container => m_gameContainer;

    [SerializeField] private Transform m_piecesContainer;

    [SerializeField] private LevelLayout m_layout;
    public LevelLayout Layout => m_layout;

    [SerializeField] private LevelData m_levelData;

    private PieceFactory m_pieceFactory;

    private List<PieceController> m_pieces = new List<PieceController>();
    public List<PieceController> Pieces => m_pieces;

    private void Awake()
    {
        if (m_instance) Destroy(gameObject);
        m_instance = this;

        m_pieceFactory = GetComponent<PieceFactory>();
    }

    private void OnDestroy()
    {
        m_instance = null;
    }

    private void Start()
    {
        //
    }

    public void LoadLevel(LevelData levelData)
    {
        Debug.Log("[GameController] Load level");

        m_levelData = levelData;

        while (m_piecesContainer.childCount > 0)
        {
            DestroyImmediate(m_piecesContainer.GetChild(0).gameObject);
        }

        m_pieces.Clear();

        foreach (LevelData.PieceData pieceData in levelData.pieces)
        {
            PieceController piece = m_pieceFactory.Create(pieceData.type);
            piece.Rotate(pieceData.rotation);
            piece.Coordinates.Set(pieceData.beginCoordinates);
            piece.Interactable = false;

            m_pieces.Add(piece);
        }

        m_layout.CreateBoard(m_levelData);
        m_layout.Layout(m_pieces);
    }

    public void StartGame()
    {
        Debug.Log("[GameController] Game started");

        foreach (PieceController piece in m_pieces)
        {
            piece.Interactable = true;
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

        MainController.Instance.ShowGameEnded();

        OnGameEnded?.Invoke(this, new EventArgs());
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

        foreach (PieceController piece in m_pieces)
        {
            connected &= piece.ConnectedToSource;
        }

        return connected;
    }
}
