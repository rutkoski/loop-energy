using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public delegate void StateChangedEvent(object sender, EventArgs args);

    public static event StateChangedEvent OnStateChanged;

    [SerializeField] private Transform m_rotate;

    [SerializeField] private SpriteRenderer[] m_glow;

    [SerializeField] private string m_type;
    public string Type => m_type;

    [Tooltip("Static pieces can not be moved/rotated")]
    [SerializeField] private bool m_isStatic;
    public bool IsStatic => m_isStatic;

    [Tooltip("Source pieces generate energy")]
    [SerializeField] private bool m_isSource;
    public bool IsSource => m_isSource;

    [Tooltip("Target pieces are at the end of a circuit")]
    [SerializeField] private bool m_isTarget;
    public bool IsTarget => m_isTarget;

    [SerializeField] private PieceCoordinates m_coordinates = new PieceCoordinates();
    public PieceCoordinates Coordinates => m_coordinates;

    [Tooltip("Enable/disable user interaction")]
    [SerializeField] private bool m_interactable;
    public bool Interactable
    {
        get => m_interactable;
        set => m_interactable = value;
    }

    private bool m_connectedToSource;
    public bool ConnectedToSource => m_isSource || m_connectedToSource;

    [SerializeField] private List<PieceController> m_connections = new List<PieceController>();

    /**
     * LayerMask for connector overlaping
     */
    private LayerMask m_mask;

    private void Awake()
    {
        m_mask = 1 << LayerMask.NameToLayer("Connector");
    }

    private void Start()
    {
        UpdateConnections();
    }

    private void Update()
    {
        if (m_glow != null && m_glow.Length > 0)
        {
            foreach (SpriteRenderer sprite in m_glow)
            {
                sprite.gameObject.SetActive(ConnectedToSource);
            }
        }
    }

    public void Rotate(float rotation)
    {
        m_rotate.Rotate(0, 0, rotation);
    }

    public void StateChanged()
    {
        OnStateChanged?.Invoke(this, new EventArgs());
    }

    /**
     * Recursively set connected pieces as connected to source
     */
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

    /**
     * Check for connected pieces
     */
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
