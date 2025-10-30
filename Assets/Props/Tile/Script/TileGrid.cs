using System.Collections.Generic;
using UnityEngine;

public class TileGrid : MonoBehaviour
{
    private const int GridSize = 7;     //盤面の大きさ(半径)

    //盤面
    private static TileDate[,] m_tileGrid = new TileDate[GridSize, GridSize];

    private void Start()
    {
        //盤面のセットアップ
        int index = 0;
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //自身の子オブジェクトを二次元配列に変換
                m_tileGrid[x, y] = transform.GetChild(index).gameObject.GetComponent<TileDate>();
                m_tileGrid[x, y].GetComponent<TileCondition>().SetCondition((TileState)Random.Range(0, (int)TileState.Poison));
                index++;
            }
        }
    }

    //盤面座標に変換
    public static Vector3 ToGridPos(Vector3 position)
    {
        int posX = Mathf.RoundToInt(position.x);
        int posZ = Mathf.RoundToInt(position.z);

        return new Vector3(posX, 0.0f, posZ);
    }

    static public int Check()
    {
        int power = 0;

        //盤面の全探索
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //非アクティブの盤面はスキップ
                if (!m_tileGrid[x, y].IsActive) continue;

                //攻撃力の加算
                if (m_tileGrid[x, y].GetComponent<TileCondition>().State == TileState.Power) power++;
            }
        }

        return power;
    }

    //盤面を全て非アクティブ状態にする
    static public void AllInactive()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                if (m_tileGrid[x, y].IsActive)
                {
                    //ランダムで状態を変更
                    m_tileGrid[x, y].GetComponent<TileCondition>().SetCondition((TileState)Random.Range(0, (int)TileState.Poison));
                    m_tileGrid[x, y].Inactive();
                }
            }
        }
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
                //アクティブの盤面は攻撃エフェクトを表示
                if (m_tileGrid[x, y].IsActive) m_tileGrid[x, y].PlayerAttack();
            }
        }
    }
}