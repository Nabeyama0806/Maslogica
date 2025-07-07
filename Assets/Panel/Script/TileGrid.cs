using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TileGrid : MonoBehaviour
{
    private const int GridSize = 7;     //�Ֆʂ̑傫��(���a)

    //�Ֆ�
    [SerializeField] private TileDate[] m_tile;
    private static TileDate[,] m_tileGrid = new TileDate[GridSize, GridSize];

    private void Start()
    {
        SetTileGrid();
    }

    private void SetTileGrid()
    {
        int index = 0;
        for (int i = 0; i < GridSize; ++i)
        {
            for (int j = 0; j < GridSize; ++j)
            {
                m_tileGrid[i, j] = m_tile[index];
                index++;
            }
        }
    }

    public static bool Flip(Vector2Int panelPos)
    {
        //�ՖʊO�Ȃ牽�����Ȃ�
        if (panelPos.x >= GridSize || panelPos.y >= GridSize) return false;

        //��Ԃ̔��]
        m_tileGrid[panelPos.x, panelPos.y].IsActive = !m_tileGrid[panelPos.x, panelPos.y].IsActive;

        return m_tileGrid[panelPos.x, panelPos.y].IsActive;
    }

    public static void PassiveAll()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //�ŏ��͑S��OFF�̏��
                m_tileGrid[y, x].IsActive = false;
            }
        }
    }
}
