using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PieceDragController : MonoBehaviour, IInitializePotentialDragHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private PieceController m_piece;

    private Camera m_camera;

    private LevelLayout Layout => GameController.Instance.Layout;

    private Vector3 m_originalPosition;

    private void Awake()
    {
        m_camera = Camera.main;
        m_piece = GetComponent<PieceController>();
    }

    public void OnInitializePotentialDrag(PointerEventData eventData)
    {
        if (m_piece.IsStatic || !m_piece.Interactable) return;

        m_originalPosition = transform.position;
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

        transform.position = position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (m_piece.IsStatic || !m_piece.Interactable) return;

        Vector3 position = transform.position;

        PieceCoordinates coord = Layout.PosToCoord(position);

        PieceController other = GameController.Instance.Pieces.Find(p => p.Coordinates == coord);

        if (other)
        {
            transform.position = m_originalPosition;
        }
        else
        {
            position = Layout.CoordToPos(coord);

            m_piece.Coordinates.Set(coord);

            transform.position = position;

            m_piece.StateChanged();
        }
    }
}
