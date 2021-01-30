using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceClickToRotateController : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int m_sides = 4;

    private PieceController m_piece;

    private float m_rotation = 0;

    private void Awake()
    {
        m_piece = GetComponent<PieceController>();

        m_rotation = -360f / m_sides;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (m_piece.IsStatic || !m_piece.Interactable) return;

        transform.Rotate(0, 0, m_rotation);

        m_piece.StateChanged();
    }
}
