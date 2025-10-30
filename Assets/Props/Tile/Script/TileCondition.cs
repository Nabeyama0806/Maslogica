using System;
using System.Collections;
using UnityEngine;

public enum TileState
{
    Normal,     //通常
    Power,      //攻撃力UP
    Defense,    //ダメージ軽減
    Energy,     //エネルギーチャージ
    Heal,       //回復
    Poison,     //毒

    Length,
}

public class TileCondition : MonoBehaviour
{
    [SerializeField] GameObject[] m_tileEffect;
    [SerializeField] GameObject m_activeEffect;
    [SerializeField] AudioClip m_activeSound;
    [SerializeField] GameObject m_playerAttackEffect;

    private TileState m_state;  //自身の状態

    public TileState State => m_state;

    public GameObject ActiveEffect => m_activeEffect;

    private void Start()
    {
        m_state = TileState.Normal;
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
}