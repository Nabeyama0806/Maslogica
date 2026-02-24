using System;
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

    [SerializeField] Vector3 m_playerPosition;
    [SerializeField] GameObject m_enemy;
    [SerializeField] GameObject m_collider;

    private GameObject m_player;
    private Phase m_phase;
    private Phase m_nextPhase;
    private CharacterStatus m_playerStatus;
    private CharacterStatus m_enemyStatus;

    private Action[] m_phaseAction;

    private void Awake()
    {
        //フェースの初期化
        m_phase = Phase.Check;
        m_nextPhase = Phase.PlayerTurn;

        //フェーズごとの関数登録
        m_phaseAction = new Action[(int)Phase.Length]
        {
            PlayerTurn,
            EnemyTurn,
            Check,
            Finish,   
        };

        //ヒエラルキー上のプレイヤーを取得
        m_player = GetObject.Instance.Player;

        //プレイヤーとエネミーのステータスを取得
        m_playerStatus = m_player.GetComponent<CharacterStatus>();
        m_enemyStatus = m_enemy.GetComponent<CharacterStatus>();

        //バトル中はプレイヤーの移動を制限 
        m_player.GetComponent<PlayerController>().IsBattle = true;
    }

    private void Update()
    {
        //現在のフェーズの処理を実行
        m_phaseAction[(int)m_phase]?.Invoke();
    }

    private void PlayerTurn()
    {
        //プレイヤーの操作が終了するまで待機
        if (!m_player.GetComponent<PlayerController>().IsTurnEnd()) return;

        //盤面の状態をチェック
        TileGrid.Check();

        //敵にダメージを与える
        m_enemyStatus.Damage(m_playerStatus);

        //次のフェーズへ
        m_nextPhase = Phase.EnemyTurn;
        m_phase = Phase.Check;
    }

    private void EnemyTurn()
    {
        //エネミーの行動が終了するまで待機
        if (!m_enemy.GetComponent<EnemyController>().IsTurnEnd()) return;

        //次のフェーズへ
        m_nextPhase = Phase.PlayerTurn;
        m_phase = Phase.Check;
    }

    private void Check()
    {
        //プレイヤーの勝利判定
        if (m_enemyStatus.Current.health <= 0)
        {
            //プレイヤーの移動制限を解除
            m_player.GetComponent<PlayerController>().IsBattle = false;

            //盤面のリセット
            TileGrid.AllClose();

            m_phase = Phase.Finish;
            return;
        }

        //エネミーの勝利判定
        if (m_playerStatus.Current.health <= 0)
        {
            //プレイヤーの移動を停止
            m_player.GetComponent<PlayerController>().enabled = false;

            //盤面のリセット
            TileGrid.AllClose();

            m_phase = Phase.Finish;
            return;
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
    }

    private void Finish()
    {
        m_collider.SetActive(false);
    }
}