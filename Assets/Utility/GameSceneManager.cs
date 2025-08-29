using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public enum Phase
    {
        PlayerTurn, 
        EnemyTurn,
        Check,
        Finish,

        Length,
    }

    [SerializeField] GameObject m_enemy;

    private GameObject m_player;
    private Phase m_phase;
    private Phase m_nextPhase;
    private CharacterStatus m_playerStatus;
    private CharacterStatus m_enemyStatus;

    private void Awake()
    {
        //フェースの初期化
        m_phase = Phase.Check;
        m_nextPhase = Phase.PlayerTurn;

        //ヒエラルキー上のプレイヤーを取得
        m_player = GetObject.Instance.Player;

        //プレイヤーとエネミーのステータスを取得
        m_playerStatus = m_player.GetComponent<CharacterStatus>();
        m_enemyStatus = m_enemy.GetComponent<CharacterStatus>();

        //バトル中はプレイヤーの移動を制限 
        m_player.GetComponent<PlayerController>().IsBattle = true;
    }

    private void FixedUpdate()
    {
        switch (m_phase)
        {
        case Phase.PlayerTurn:
                //プレイヤーの操作が終了するまで待機
                if (!m_player.GetComponent<PlayerController>().IsTurnEnd()) break;

                //敵にダメージを与える
                if (TileGrid.Check()) m_enemyStatus.Damage(m_playerStatus.Power);

                //次のフェーズへ
                m_nextPhase = Phase.EnemyTurn;
                m_phase = Phase.Check;
                break;

        case Phase.EnemyTurn:
                //エネミーの行動が終了するまで待機
                if (!m_enemy.GetComponent<EnemyController>().IsTurnEnd()) break;

                //次のフェーズへ
                m_nextPhase = Phase.PlayerTurn;
                m_phase = Phase.Check;
                break;

        case Phase.Check:
                //プレイヤーの勝利判定
                if (m_enemyStatus.CurrentHealth <= 0)
                {
                    //プレイヤーの移動制限を解除
                    m_player.GetComponent<PlayerController>().IsBattle = false;

                    m_phase = Phase.Finish;
                    break;
                }

                //エネミーの勝利判定
                if (m_playerStatus.CurrentHealth <= 0)
                {
                    //プレイヤーの移動を停止
                    m_player.GetComponent<PlayerController>().enabled = false;

                    m_phase = Phase.Finish;
                    break;
                }

                //次のターンの準備
                m_phase = m_nextPhase;
                if (m_phase == Phase.PlayerTurn)
                {
                    m_player.GetComponent<PlayerController>().Play();
                }
                else 
                {
                    m_enemy.GetComponent<EnemyController>().Play();
                }

                TileGrid.AllInactive();
                break;

        case Phase.Finish:
                //盤面のリセット
                TileGrid.AllReset();
                break;
        }
    }
}