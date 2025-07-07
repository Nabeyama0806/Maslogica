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

    //���܂ꂽ��
    public static void IsActive(Vector2 panelPos)
    {
        //�ՖʊO�Ȃ牽�����Ȃ�
        if ((int)panelPos.x >= GridSize || (int)panelPos.y >= GridSize) return;

        //��Ԃ̔��]
        m_tileGrid[(int)panelPos.x, (int)panelPos.y].IsActive = !m_tileGrid[(int)panelPos.x, (int)panelPos.y].IsActive;
    }

    //�S�Ẵt���O��܂�
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

    public static TileDate GetTile(Vector2 tilePos)
    {
        //�^�C���̏���
        return m_tileGrid[(int)tilePos.x, (int)tilePos.y];
    }
}
