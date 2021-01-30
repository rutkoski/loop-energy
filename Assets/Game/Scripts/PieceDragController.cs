using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceDragController : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private PieceController m_piece;

    private Camera m_camera;

    private LevelLayout Layout => GameController.Instance.Layout;

    private void Awake()
    {
        m_camera = Camera.main;
        m_piece = GetComponent<PieceController>();
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (m_piece.IsStatic || !m_piece.Interactable) return;

        //
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (m_piece.IsStatic || !m_piece.Interactable) return;

        //
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_piece.IsStatic || !m_piece.Interactable) return;

        Vector3 position = eventData.position;
        position.z = transform.position.z - m_camera.transform.position.z;
        position = m_camera.ScreenToWorldPoint(position);

        //position = Layout.Normalize(position);

        transform.position = position;

        //m_piece.StateChanged();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_piece.IsStatic || !m_piece.Interactable) return;

        Vector3 position = transform.position;

        position = Layout.Normalize(position);

        transform.position = position;

        m_piece.StateChanged();
    }
}
