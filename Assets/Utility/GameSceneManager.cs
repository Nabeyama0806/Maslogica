using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] PlayerController m_player;
    [SerializeField] EnemyController m_enemy;

    public enum Phase
    {
        Start,
        PlayerTurn, 
        EnemyTurn,
        Check,
        Finish,

        Length,
    }

    private Phase m_phase;
    private Phase m_nextPhase;

    private void Awake()
    {
        //マウスカーソルを非表示にする
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        //開始のフェード
        Fade.FadeIn(1.5f);
        m_phase = Phase.Start;
    }

    private void FixedUpdate()
    {
        Debug.Log(m_phase);

        switch (m_phase)
        {
        case Phase.Start:
                Debug.Log("ゲームスタート");
                m_phase = Phase.Check;
                m_nextPhase = Phase.PlayerTurn;
                break;

        case Phase.PlayerTurn:
                //プレイヤーの操作が終了するまで待機
                if (!m_player.IsTurnEnd()) break;

                //敵にダメージを与える
                if (TileGrid.Check()) m_enemy.GetComponent<Health>().Damage(10);

                //次のフェーズへ
                m_nextPhase = Phase.EnemyTurn;
                m_phase = Phase.Check;
                break;

        case Phase.EnemyTurn:
                //エネミーの行動が終了するまで待機
                if (m_enemy.Play()) break;

                //プレイヤーにダメージを与える
                Vector2Int pos = new Vector2Int((int)m_player.transform.position.x, (int)m_player.transform.position.x);
                if (TileGrid.IsEnemyAttack(pos)) m_player.GetComponent<Health>().Damage(10);

                //次のフェーズへ
                m_nextPhase = Phase.PlayerTurn;
                m_phase = Phase.Check;
                break;

        case Phase.Check:
                //勝敗の確認
                if (m_player.GetComponent<Health>().Value <= 0
                ||  m_enemy.GetComponent<Health>().Value <= 0)
                {
                    m_phase = Phase.Finish;
                    break;
                }

                //次のターンを取得
                m_phase = m_nextPhase;
                
                //次のターンの準備
                if (m_phase == Phase.PlayerTurn) m_player.Play();
                if (m_phase == Phase.EnemyTurn) m_enemy.Play();

                //盤面のリセット
                TileGrid.AllReset();
                break;

        case Phase.Finish:
                break;
        }
    }
}