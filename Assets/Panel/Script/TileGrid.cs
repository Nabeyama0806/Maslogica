using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TileGrid : MonoBehaviour
{
    private const int GridSize = 8;     //盤面の大きさ(半径)

    //盤面
    [SerializeField] private TileDate[] m_tile;
    private static TileDate[,] m_tileGrid = new TileDate[GridSize, GridSize];

    private void Start()
    {
        for (int i = 0; i < GridSize; ++i)
        {
            for (int j = 0; j < GridSize; ++j)
            {
                //m_tileGrid[i, j] = m_tile[i + j * GridSize];
            }
        }
    }

    //踏まれたら
    static public void IsActive(Vector2 panelPos)
    {
        //盤面外なら何もしない
        if ((int)panelPos.x >= GridSize || (int)panelPos.y >= GridSize) return;

        //状態の反転
        m_tileGrid[(int)panelPos.x, (int)panelPos.y].IsActive = !m_tileGrid[(int)panelPos.x, (int)panelPos.y].IsActive;
    }

    //全てのフラグを折る
    static public void PassiveAll()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //最初は全てOFFの状態
                m_tileGrid[y, x].IsActive = false;
            }
        }
    }
}
