using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    private const int GridSize = 7;     //盤面の大きさ(半径)

    //盤面
    private static TileDate[,] m_tileGrid = new TileDate[GridSize, GridSize];

    //エネミーの攻撃範囲のリスト
    private static List<int> m_enemyAttackGrid = new List<int>();

    private void Start()
    {
        SetupTileGrid();

        for (int i = 0; i < GridSize; ++i)
        {
            m_enemyAttackGrid.Add(i);
        }
    }

    private void SetupTileGrid()
    {
        int index = 0;
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //自身の子オブジェクトを二次元配列に変換
                m_tileGrid[x, y] = transform.GetChild(index).gameObject.GetComponent<TileDate>();
                index++;
            }
        }
    }

    static public bool Check()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //非アクティブの盤面はスキップ
                if (!m_tileGrid[x, y].IsActive) continue;

                //列の判定
                if (IsLine(x, y)) return true;
            }
        }

        return false;
    }

    static private bool IsLine(int x, int y)
    {
        int count = 0;

        //横の列
        for (int i = 0; i < GridSize; ++i)
        {
            if (!m_tileGrid[i, y].IsActive) break;
            count++;
        }

        if (count == GridSize) return true;

        count = 0;

        //縦の列
        for (int i = 0; i < GridSize; ++i)
        {
            if (!m_tileGrid[x, i].IsActive) break;
            count++;
        }

        if (count == GridSize) return true;

        return false;
    }

    //盤面座標に変換
    public static Vector3 ToGridPos(Vector3 position)
    {
        int posX = Mathf.RoundToInt(position.x);
        int posZ = Mathf.RoundToInt(position.z);

        return new Vector3(posX, 0.0f, posZ);
    }

    //ランダムで攻撃範囲を選択
    static public void RandomSelect()
    {
        int rand = Random.Range(0, m_enemyAttackGrid.Count);

        //横の列
        for (int i = 0; i < GridSize; ++i)
        {
            m_tileGrid[i, m_enemyAttackGrid[rand]].EnemyAttack();
        }

        //縦の列
        for (int i = 0; i < GridSize; ++i)
        {
            m_tileGrid[m_enemyAttackGrid[rand], i].EnemyAttack();
        }

        //選択した攻撃範囲をリストから削除
        m_enemyAttackGrid.Remove(rand);
    }

    //盤面を全て非アクティブ状態にする
    public static void AllReset()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                m_tileGrid[x, y].Close();
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
                if (m_tileGrid[x, y].IsActive) m_tileGrid[x, y].PlayerAttack();
               
            }
        }
    }

    static public void AllInactive()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                if (m_tileGrid[x, y].IsActive) m_tileGrid[x, y].Inactive();
            }
        }
    }

    static public void DrawShape(int cardIndex)
    {
        //選択されたカードの形状を盤面に表示

    }
}