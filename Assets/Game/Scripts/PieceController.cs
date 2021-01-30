using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public delegate void StateChangedEvent(object sender, EventArgs args);

    public static event StateChangedEvent OnStateChanged;

    [SerializeField] private string m_type;
    public string Type => m_type;

    //[SerializeField] private int m_sides = 4;

    [SerializeField] private bool m_isStatic;
    public bool IsStatic => m_isStatic;

    [SerializeField] private bool m_isSource;
    public bool IsSource => m_isSource;

    [SerializeField] private bool m_isTarget;
    public bool IsTarget => m_isTarget;

    [SerializeField] private PieceCoodinates m_coordinates = new PieceCoodinates();
    public PieceCoodinates Coordinates => m_coordinates;

    private bool m_interactable;
    public bool Interactable
    {
        get => m_interactable;
        set => m_interactable = value;
    }

    //private float m_rotation = 0;

    private bool m_connectedToSource;
    public bool ConnectedToSource => m_isSource || m_connectedToSource;

    [SerializeField] private List<PieceController> m_connections = new List<PieceController>();

    private static List<PieceController> m_pieces = new List<PieceController>();

    private LayerMask m_mask;

    private SpriteRenderer m_spriteRenderer;

    private void OnEnable()
    {
        m_pieces.Add(this);
    }

    private void OnDisable()
    {
        m_pieces.Remove(this);
    }

    private void Awake()
    {
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        m_mask = 1 << LayerMask.NameToLayer("Connector");

        //m_rotation = -360f / m_sides;
    }

    private void Start()
    {
        UpdateConnections();
    }

    private void Update()
    {
        m_spriteRenderer.color = ConnectedToSource ? Color.green : Color.red;
    }

    public void StateChanged()
    {
        OnStateChanged?.Invoke(this, new EventArgs());
    }

    public void SetConnectedToSource(List<PieceController> visited)
    {
        visited.Add(this);

        m_connectedToSource = true;

        foreach (PieceController other in m_connections)
        {
            if (visited.Contains(other)) continue;

            other.SetConnectedToSource(visited);
        }
    }

    public void UpdateConnections()
    {
        m_connectedToSource = false;

        m_connections.Clear();

        PieceConnector[] connectors = GetComponentsInChildren<PieceConnector>();

        foreach (PieceConnector connector in connectors)
        {
            Vector2 point = connector.transform.position;

            Collider2D[] overlaps = Physics2D.OverlapPointAll(point, m_mask);

            foreach (Collider2D overlap in overlaps)
            {
                PieceConnector other = overlap.GetComponent<PieceConnector>();

                if (other && other != connector)
                {

                    PieceController parent = other.GetComponentInParent<PieceController>();

                    m_connections.Add(parent);
                }
            }
        }
    }
}
