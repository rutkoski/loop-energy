using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieceFactory))]
public class GameController : MonoBehaviour
{
    private static GameController m_instance;
    public static GameController Instance => m_instance;

    [SerializeField] private Transform m_board;

    [SerializeField] private LevelLayout m_layout;
    public LevelLayout Layout => m_layout;

    [SerializeField] private LevelData m_levalData;

    private PieceFactory m_pieceFactory;

    private List<PieceController> m_pieces = new List<PieceController>();

    private List<PieceController> m_targets = new List<PieceController>();

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
        //EndGame();

        LoadLevel(m_levalData);

        StartGame();
    }

    public void LoadLevel(LevelData levelData)
    {
        while (m_board.childCount > 0 && m_board.GetChild(0) is Transform child)
        {
            Destroy(child);
        }

        m_pieces.Clear();
        m_targets.Clear();

        foreach (LevelData.PieceData pieceData in levelData.pieces)
        {
            PieceController piece = m_pieceFactory.Create(pieceData.type);
            piece.transform.rotation = Quaternion.Euler(0, 0, pieceData.rotation);
            piece.Coordinates.Set(pieceData.coordinates);
            piece.Interactable = false;

            m_pieces.Add(piece);
            
            if (piece.IsTarget)
            {
                m_targets.Add(piece);
            }
        }

        m_layout.Layout(m_pieces);
    }

    private void StartGame()
    {
        Debug.Log("[GameController] Game started");

        //m_pieces.Clear();
        //m_pieces.AddRange(m_board.GetComponentsInChildren<PieceController>());

        //m_targets.Clear();

        foreach (PieceController piece in m_pieces)
        {
            piece.Interactable = true;

            //if (piece.IsTarget)
            //{
            //    m_targets.Add(piece);
            //}
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
