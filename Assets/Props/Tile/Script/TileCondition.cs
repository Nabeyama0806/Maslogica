using System;
using System.Collections;
using UnityEngine;

public enum TileState
{
    Power,      //攻撃力UP
    Defense,    //ダメージ軽減
    Heal,       //回復

    Length,
}

public enum TileEffect
{
    Normal,     
    Active,     
    Length,
}

public class TileCondition : MonoBehaviour
{
    private const int PowerUpValue = 3;     //攻撃力UPの値
    private const int DefenseUpValue = 2;   //防御力UPの値
    private const int HealValue = 2;        //回復の値

    [SerializeField] GameObject[] m_tileEffect;
    [SerializeField] GameObject m_activeEffect;
    [SerializeField] AudioClip m_activeSound;
    [SerializeField] GameObject m_playerAttackEffect;

    private PlayerStatus m_playerStatus;
    private TileState m_state;  //自身の状態

    public TileState State => m_state;

    public GameObject ActiveEffect => m_activeEffect;

    private Action[] m_condition;

    private void Start()
    {
        //プレイヤーの取得
        m_playerStatus = GetObject.Instance.Player.GetComponent<PlayerStatus>();

        //状態ごとの関数登録
        m_condition = new Action[(int)TileState.Length]
        {
            PowerState,
            DefenseState,
            HealState,
        };
    }

    //状態の変化
    public void SetCondition(TileState conditionType)
    {
        //現在のエフェクトを非表示
        m_tileEffect[(int)m_state].SetActive(false);

        //状態変化
        m_state = conditionType;

        //変化した状態のエフェクトを表示
        m_tileEffect[(int)m_state].SetActive(true);
    }

    //全てのエフェクトを非表示
    public void AllEffectOff()
    {
        for (int i = 0; i < m_tileEffect.Length; i++)
        {
            m_tileEffect[i].SetActive(false);
        }
    }

    //プレイヤーの攻撃エフェクト
    public IEnumerator PlayerAttackEffect()
    { 
        m_playerAttackEffect.SetActive(true);

        yield return new WaitForSeconds(0.7f);

        m_playerAttackEffect.SetActive(false);
    }

    public void IsActive(bool active)
    {
        m_activeEffect.SetActive(active);
        SoundManager.Play2D(m_activeSound, 0.5f);
    }

    public void CheckCondition()
    {
        //状態ごとの処理実行
        m_condition[(int)m_state]();
    }

    private void PowerState()
    {
        //攻撃力UP
        m_playerStatus.Power.Add(PowerUpValue);
    }

    private void DefenseState()
    {
        //防御力UP
        m_playerStatus.Power.Add(DefenseUpValue);
    }

    private void HealState()
    {
        //回復
        m_playerStatus.Heal(HealValue);
    }
}