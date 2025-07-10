using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TileGrid : MonoBehaviour
{
    private const int GridSize = 7;     //盤面の大きさ(半径)

    //盤面
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

    //盤面座標に変換
    public static Vector3 ToGridPos(Vector3 position)
    {
        int posX = Mathf.RoundToInt(position.x);
        int posZ = Mathf.RoundToInt(position.z);

        //盤面外なら左上に修正
        if (posX >= GridSize || posZ >= GridSize) return new Vector3();

        return new Vector3(posX, position.y, posZ);
    }

    //状態の反転
    public static bool Flip(Vector2Int panelPos)
    {
        //盤面外なら何もしない
        if (panelPos.x >= GridSize || panelPos.y >= GridSize) return false;

        //状態の反転
        m_tileGrid[panelPos.x, panelPos.y].IsActive = !m_tileGrid[panelPos.x, panelPos.y].IsActive;

        return m_tileGrid[panelPos.x, panelPos.y].IsActive;
    }

    //ランダムで一マス選択
    static public void RandomSelect()
    {
        int randX = Random.Range(0, GridSize);
        int randY = Random.Range(0, GridSize);

        m_tileGrid[randX, randY].EnemyAttack();
    }

    //盤面のリセット
    public static void PassiveAll()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //全てOFFの状態
                if (m_tileGrid[y, x].IsActive) m_tileGrid[y, x].Passive();
            }
        }
    }

    static public void PlayEffect()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //アクティブの盤面はエフェクトを表示
                if (m_tileGrid[y, x].IsActive) m_tileGrid[y, x].PlayEffect(TileDate.EffectType.PlayerAttack);
            }
        }
    }
}
