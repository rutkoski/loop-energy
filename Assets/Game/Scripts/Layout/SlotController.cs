using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotController : MonoBehaviour
{
    private bool m_focus;
    public bool Focus
    {
        get => m_focus;
        set => m_focus = value;
    }

    private PieceCoordinates m_coordinates;
    public PieceCoordinates Coordinates
    {
        get => m_coordinates;
        set => m_coordinates = value;
    }

    private SpriteRenderer m_spriteRenderer;

    private void Awake()
    {
        m_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public void SetFocus(bool focus)
    {
        m_focus = focus;
    }

    private void Update()
    {
        m_spriteRenderer.color = m_focus ? Color.blue : Color.cyan;

        m_focus = false;
    }
}
