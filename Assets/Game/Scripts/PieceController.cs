﻿using System;
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

    [SerializeField] private bool m_isStatic;
    public bool IsStatic => m_isStatic;

    [SerializeField] private bool m_isSource;
    public bool IsSource => m_isSource;

    [SerializeField] private bool m_isTarget;
    public bool IsTarget => m_isTarget;

    [SerializeField] private PieceCoordinates m_coordinates = new PieceCoordinates();
    public PieceCoordinates Coordinates => m_coordinates;

    [SerializeField] private bool m_interactable;
    public bool Interactable
    {
        get => m_interactable;
        set => m_interactable = value;
    }

    private bool m_connectedToSource;
    public bool ConnectedToSource => m_isSource || m_connectedToSource;

    [SerializeField] private List<PieceController> m_connections = new List<PieceController>();

    private static List<PieceController> m_pieces = new List<PieceController>();

    private LayerMask m_mask;

    public enum State
    {
        Init,
        Idle,
        FadeIn,
        FadeOut,
        FocusIn,
        FocusOut,
    }

    private State m_state = State.Init;
    public State CurrentState
    {
        get => m_state;
        set => m_state = value;
    }

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
