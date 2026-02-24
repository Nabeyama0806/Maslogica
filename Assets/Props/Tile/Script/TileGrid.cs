using UnityEngine;

public class TileGrid : MonoBehaviour
{
    private const int GridSize = 5;     //盤面の大きさ

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
                m_tileGrid[x, y].GetComponent<TileCondition>().SetCondition((TileState)Random.Range(0, (int)TileState.Length));
                index++;
            }
        }

        //プレイヤーの初期位置を盤面内のランダムな位置に設定
        Vector3 playerPos = new Vector3(Random.Range(0, GridSize), 1, Random.Range(0, GridSize));
        GetObject.Instance.Player.transform.position = playerPos;
    }

    //盤面座標に変換
    public static Vector3 ToGridPos(Vector3 position)
    {
        int posX = Mathf.RoundToInt(position.x);
        int posZ = Mathf.RoundToInt(position.z);

        return new Vector3(posX, 0.0f, posZ);
    }

    static public void Check()
    {
        //盤面の全探索
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //非アクティブの盤面はスキップ
                if (!m_tileGrid[x, y].IsActive) continue;

                //状態ごとの処理を実行
                m_tileGrid[x, y].GetComponent<TileCondition>().CheckCondition();
            }
        }
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
                    m_tileGrid[x, y].GetComponent<TileCondition>().SetCondition((TileState)Random.Range(0, (int)TileState.Length));
                    m_tileGrid[x, y].Inactive();
                }
            }
        }
    }

    //盤面を閉じる
    public static void AllClose()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                m_tileGrid[x, y].Close();
            }
        }
    }

    //プレイヤーの攻撃エフェクトを再生
    static public void PlayEffect()
    {
        for (int y = 0; y < GridSize; ++y)
        {
            for (int x = 0; x < GridSize; ++x)
            {
                //アクティブの盤面は攻撃エフェクトを表示
                if (m_tileGrid[x, y].IsActive) m_tileGrid[x, y].StartCoroutine(m_tileGrid[x, y].GetComponent<TileCondition>().PlayerAttackEffect());
            }
        }
    }
}