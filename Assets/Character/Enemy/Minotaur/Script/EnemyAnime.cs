using UnityEngine;

public class EnemyAnime : MonoBehaviour
{
    static private EnemyAnime m_instance;

    static public EnemyAnime Instance
    {
        get { return m_instance; }
    }

    [SerializeField] EnemyController m_controller;
    [SerializeField] GameObject m_ui;
    [SerializeField] GameObject m_wall;
    [SerializeField] GameObject m_gatePortal;
    [SerializeField] GameObject m_deathEffect;
    [SerializeField] AudioClip m_attack;

    private Animator m_animator;

    private void Awake()
    {
        m_instance = this;
        m_animator = GetComponent<Animator>();
        m_gatePortal.SetActive(false);
        m_wall.SetActive(true);
    }

    public void Attack()
    {
        m_animator.SetTrigger("Attack");

        //攻撃エフェクト
    }
    public void AttackEnd()
    {
        //次の攻撃マスをランダムで選択
        TileGrid.RandomSelect();

        //効果音
        SoundManager.Play2D(m_attack);
    }

    public void NextAttackEnd()
    {
        //ターン終了
        m_controller.IsTurnEndFlag = true;
    }

    public void Death()
    {
        m_animator.SetBool("Death", true);

        m_ui.SetActive(false);
    }
    public void DeathEnd()
    {
        //盤面のリセット
        TileGrid.AllReset();

        //死亡エフェクトを生成
        Instantiate(m_deathEffect, m_gatePortal.transform.position, Quaternion.identity);

        //シーン遷移用のゲートを表示
        m_gatePortal.SetActive(true);

        //移動制限用の壁を非表示
        m_wall.SetActive(false);

        //自身の削除
        Destroy(gameObject);
    }

    public void Damage()
    {
        m_animator.SetTrigger("Damage");
    }
}