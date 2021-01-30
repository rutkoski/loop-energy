using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int m_sides = 4;

    [SerializeField] private bool m_isStatic;

    [SerializeField] private bool m_isSource;

    [SerializeField] private bool m_isTarget;

    private float m_rotation = 0;

    private bool m_connectedToSource;
    public bool ConnectedToSource => m_isSource || m_connectedToSource;

    [SerializeField] private List<PieceController> m_connections = new List<PieceController>();

    private static List<PieceController> m_pieces = new List<PieceController>();

    public LayerMask m_mask;

    private bool m_dirty;

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
        m_rotation = 360f / m_sides;
    }

    private void Start()
    {
        UpdateConnections();
    }

    private void Update()
    {
        GetComponentInChildren<SpriteRenderer>().color = ConnectedToSource ? Color.green : Color.red;
    }

    private void FixedUpdate()
    {
        if (m_dirty)
        {

            List<PieceController> visited = new List<PieceController>();

        foreach (PieceController piece in m_pieces)
        {
            piece.UpdateConnections();
        }

        foreach (PieceController piece in m_pieces)
        {
            if (piece.m_isSource)
            {
                piece.UpdateState(visited);
            }
        }


            m_dirty = false;
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if (m_isStatic) return;

        transform.Rotate(0, 0, m_rotation);

        m_dirty = true;

        Physics2D.SyncTransforms();
    }

    private void UpdateState(List<PieceController> visited)
    {
        visited.Add(this);

        m_connectedToSource = true;

        foreach (PieceController other in m_connections)
        {
            if (visited.Contains(other)) continue;

            other.UpdateState(visited);
        }
    }

    private void UpdateConnections()
    {
        m_connectedToSource = false;

        m_connections.Clear();

        PieceConnector[] connectors = GetComponentsInChildren<PieceConnector>();

        foreach (PieceConnector connector in connectors)
        {
            Vector2 point = connector.transform.position;

            Collider2D[] overlaps = Physics2D.OverlapPointAll(point, m_mask);
            //Debug.Log(this + " " + connector + " " + overlaps.Length);
            foreach (Collider2D overlap in overlaps)
            {
                //Debug.Log(this + " " + overlap);
                
                PieceConnector other = overlap.GetComponent<PieceConnector>();

                Debug.Log(this + " " + overlap + " " + other);

                if (other && other != connector)
                {

                    PieceController parent = other.GetComponentInParent<PieceController>();

                    //Debug.Log(this + " " + parent);

                    m_connections.Add(parent);
                }
            }
        }

        //Debug.Log(this + " " + m_connections.Count);
    }
}
