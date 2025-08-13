using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    public enum Phase
    {
        Start,
        PlayerTurn, 
        EnemyTurn,
        Check,
        Finish,

        Length,
    }

    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_enemy;

    private Phase m_phase;
    private Phase m_nextPhase;
    private CharacterStatus m_playerStatus;
    private CharacterStatus m_enemyStatus;
    private void Awake()
    {
        m_phase = Phase.Start;

        m_playerStatus = m_player.GetComponent<CharacterStatus>();
        m_enemyStatus = m_enemy.GetComponent<CharacterStatus>();
    }

    private void FixedUpdate()
    {
        switch (m_phase)
        {
        case Phase.Start:
                Debug.Log(m_playerStatus.Health);
                m_phase = Phase.Check;
                m_nextPhase = Phase.PlayerTurn;
                break;

        case Phase.PlayerTurn:
                //プレイヤーの操作が終了するまで待機
                if (!m_player.GetComponent<PlayerController>().IsTurnEnd()) break;

                //敵にダメージを与える
                if (TileGrid.Check()) m_enemyStatus.Damage(m_playerStatus.Value.Power);

                //次のフェーズへ
                m_nextPhase = Phase.EnemyTurn;
                m_phase = Phase.Check;
                break;

        case Phase.EnemyTurn:
                //エネミーの行動が終了するまで待機
                if (!m_enemy.GetComponent<EnemyController>().IsTurnEnd()) break;

                //プレイヤーにダメージを与える
                m_playerStatus.Damage(m_enemyStatus.Value.Power);

                //次のフェーズへ
                m_nextPhase = Phase.PlayerTurn;
                m_phase = Phase.Check;
                break;

        case Phase.Check:
                //勝敗の確認
                if (m_playerStatus.Health <= 0
                || m_enemyStatus.Health <= 0)
                {
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

                //盤面のリセット
                TileGrid.AllReset();
                break;

        case Phase.Finish:
                //これまで保存していたプレイヤーの体力データを削除
                PlayerPrefs.DeleteKey("PlayerHealth");

                //現在のプレイヤーの体力を保持
                PlayerPrefs.SetInt("PlayerHealth", m_playerStatus.Health);
                PlayerPrefs.Save();
                break;
        }
    }
}