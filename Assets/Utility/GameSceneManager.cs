using UnityEngine;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_enemy;

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
        m_phase = Phase.Start;
    }

    private void FixedUpdate()
    {
        switch (m_phase)
        {
        case Phase.Start:
                m_phase = Phase.Check;
                m_nextPhase = Phase.PlayerTurn;
                break;

        case Phase.PlayerTurn:
                //プレイヤーの操作が終了するまで待機
                if (!m_player.GetComponent<PlayerController>().IsTurnEnd()) break;

                //敵にダメージを与える
                if (TileGrid.Check()) m_enemy.GetComponent<CharacterStatus>().Damage(m_player.GetComponent<CharacterStatus>().Value.Power);

                //次のフェーズへ
                m_nextPhase = Phase.EnemyTurn;
                m_phase = Phase.Check;
                break;

        case Phase.EnemyTurn:
                //エネミーの行動が終了するまで待機
                if (!m_enemy.GetComponent<EnemyController>().IsTurnEnd()) break;

                //プレイヤーにダメージを与える
                m_player.GetComponent<CharacterStatus>().Damage(m_enemy.GetComponent<CharacterStatus>().Value.Power);

                //次のフェーズへ
                m_nextPhase = Phase.PlayerTurn;
                m_phase = Phase.Check;
                break;

        case Phase.Check:
                //勝敗の確認
                if (m_player.GetComponent<CharacterStatus>().Value.Power <= 0
                ||  m_enemy.GetComponent<CharacterStatus>().Value.Power <= 0)
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
                break;
        }
    }
}