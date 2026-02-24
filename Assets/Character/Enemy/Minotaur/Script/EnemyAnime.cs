using System.Collections;
using UnityEngine;

public class EnemyAnime : MonoBehaviour
{
    static private EnemyAnime m_instance;

    static public EnemyAnime Instance
    {
        get { return m_instance; }
    }

    [SerializeField] EnemyController m_controller;
    [SerializeField] CharacterStatus m_status;
    [SerializeField] GameObject m_gatePortal;
    [SerializeField] GameObject m_deathEffect;
    [SerializeField] AudioClip m_tileAttack;
    [SerializeField] AudioClip m_slash;

    private Animator m_animator;
    private GameObject m_player;

    private void Awake()
    {
        m_instance = this;
        m_animator = GetComponent<Animator>();
        m_gatePortal.SetActive(false);

        //プレイヤーの取得
        m_player = GetObject.Instance.Player;
    }

    public IEnumerator Attack()
    {
        yield return new WaitForSeconds(0.6f);

        _Attack();
    }

    public void AttackEnd()
    {
        //効果音
        SoundManager.Play2D(m_slash);

        //プレイヤーにダメージを与える
        m_player.GetComponent<CharacterStatus>().Damage(m_status.Base.power);
    }

    public void NextAttack()
    {
        //マスをランダムでさせる

        //効果音
        SoundManager.Play2D(m_tileAttack);
    }

    public void NextAttackEnd()
    {
        //ターン終了
        m_controller.IsTurnEndFlag = true;
    }

    public void Death()
    {
        //死亡アニメーション
        m_animator.SetBool("Death", true);
    }

    public void DeathEnd()
    {
        //死亡エフェクトを生成
        Instantiate(m_deathEffect, m_gatePortal.transform.position, Quaternion.identity);

        //シーン遷移用のゲートを表示
        m_gatePortal.SetActive(true);

        //自身の削除
        Destroy(gameObject);
    }

    public void Damage()
    {
        m_animator.SetTrigger("Damage");
    }

    private void _Attack()
    {
        //攻撃アニメーション
        m_animator.SetTrigger("Attack");
    }
}