using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class EnemyAnime : MonoBehaviour
{
    static private EnemyAnime m_instance;

    static public EnemyAnime Instance
    {
        get { return m_instance; }
    }

    [SerializeField] EnemyController m_controller;
    [SerializeField] AudioClip m_attack;

    private Animator m_animator;

    private void Awake()
    {
        m_instance = this;
        m_animator = GetComponent<Animator>();
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
    }
    public void DeathEnd()
    {
        //シーン遷移
        SceneController.Transition("Select");
    }

    public void Damage()
    {
        m_animator.SetTrigger("Damage");
    }
}