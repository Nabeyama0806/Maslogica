using UnityEngine;

public class TileGrid : MonoBehaviour
{
    private const int GridSize = 7;     //盤面の大きさ(半径)

    //盤面
    private static TileDate[,] m_tileGrid = new TileDate[GridSize, GridSize];

    private void Start()
    {
        SetupTileGrid();
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

    static public bool IsEnemyAttack(Vector2Int playerPos)
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //攻撃マス以外はスキップ
                if (!m_tileGrid[x, y].IsEnemyAttack) continue;

                //横の列
                for (int i = 0; i < GridSize; ++i)
                {
                    //エネミーの攻撃範囲
                }

                //縦の列
                for (int i = 0; i < GridSize; ++i)
                {
                    //エネミーの攻撃範囲
                }

                //縦も横も範囲外の時は当たらない
                if (x != playerPos.x && y != playerPos.y) continue;

                return true;
            }
        }

        return false;
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

    //ランダムで一マス選択
    static public void RandomSelect()
    {
        int randX = Random.Range(0, GridSize);
        int randY = Random.Range(0, GridSize);

        m_tileGrid[randX, randY].EnemyAttack();
    }

    //盤面を全て非アクティブ状態にする
    public static void AllReset()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                if (m_tileGrid[x, y].IsActive) m_tileGrid[x, y].Inactive();
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
                if (m_tileGrid[x, y].IsActive)
                {
                    m_tileGrid[x, y].GetComponent<TileEffects>().StartCoroutine(m_tileGrid[x, y].GetComponent<TileEffects>().AutoPlay(TileEffects.EffectType.PlayerAttack));
                }
            }
        }
    }

    static public bool IsTileActive(Vector2 pos)
    { 
        return m_tileGrid[(int)pos.x, (int)pos.y].IsActive;
    }
}