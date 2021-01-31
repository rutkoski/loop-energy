using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquareLayout : LevelLayout
{
    [SerializeField] private Vector3 m_cellSize;

    [SerializeField] private Vector3 m_gridSize;

    [SerializeField] private Transform m_grid;

    [SerializeField] private SlotController m_slotPrefab;

    private LayerMask m_slotMask;

    private void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        int w = Mathf.FloorToInt(m_gridSize.x / 2);
        int h = Mathf.FloorToInt(m_gridSize.y / 2);

        for (int i = -w; i <= w; i++)
        {
            for (int j = -h; j <= h; j++)
            {
                PieceCoordinates coord = new PieceCoordinates();
                coord.x = i;
                coord.y = j;

                SlotController slot = Instantiate(m_slotPrefab, m_grid, false);

                Vector3 position = CoordToPos(coord);

                slot.transform.localPosition = position;
            }
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

        if (hit.transform != null) {
            hit.transform.GetComponent<SlotController>()?.SetFocus(true);
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
}
