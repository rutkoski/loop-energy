using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * 
 * Layouts pieces in a grid.
 * 
 * Coordinates used are x, y. 0,0 is center of screen, -1,-1 is down,left and 1,1 is right,up.
 * 
 */
public class SquareLayout : LevelLayout
{
    [SerializeField] private Vector3 m_cellSize;

    [SerializeField] private Transform m_boardContainer;

    [SerializeField] private SlotController m_slotPrefab;

    private LayerMask m_slotMask;

    private SlotController m_currentSlot;

    private List<SlotController> m_slots = new List<SlotController>();

    public override void CreateBoard(LevelData levelData)
    {
        foreach (SlotController slot in m_slots)
        {
            DestroyImmediate(slot.gameObject);
        }

        m_slots.Clear();

        foreach (PieceCoordinates coord in levelData.board)
        {
            Vector3 position = CoordToPos(coord);

            SlotController slot = Instantiate(m_slotPrefab, m_boardContainer, false);
            slot.Coordinates = coord;
            slot.transform.localPosition = position;

            m_slots.Add(slot);
        }
    }

    private void Awake()
    {
        m_slotMask = 1 << LayerMask.NameToLayer("Slot");
    }

    private void Update()
    {
        RaycastHit2D hit;

        Vector3 pos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(pos);

        hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, m_slotMask);

        m_currentSlot?.SetFocus(false);

        if (hit.transform != null)
        {
            m_currentSlot = hit.transform.GetComponent<SlotController>();
            m_currentSlot.SetFocus(true);
        }
    }

    public override Vector3 CoordToPos(PieceCoordinates coord)
    {
        Vector3 pos = new Vector3();

        pos.x = coord.x * m_cellSize.x;
        pos.y = coord.y * m_cellSize.y;
        pos.z = 0;

        return pos;
    }

    public override PieceCoordinates PosToCoord(Vector3 position)
    {
        PieceCoordinates coord = new PieceCoordinates();
        coord.x = (int)Mathf.Round(position.x / m_cellSize.x);
        coord.y = (int)Mathf.Round(position.y / m_cellSize.y);
        coord.z = 0;

        return coord;
    }

    public override Vector3 Normalize(Vector3 position)
    {
        return CoordToPos(PosToCoord(position));
    }

    public override void Layout(List<PieceController> pieces)
    {
        foreach (PieceController piece in pieces)
        {
            piece.transform.position = CoordToPos(piece.Coordinates);
        }
    }

    public override SlotController GetSlotAt(PieceCoordinates coord)
    {
        return m_slots.Find(slot => slot.Coordinates == coord);
    }
}
