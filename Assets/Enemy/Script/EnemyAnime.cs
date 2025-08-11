using UnityEngine;

public class EnemyAnime : MonoBehaviour
{
    static private EnemyAnime m_instance;

    static public EnemyAnime Instance
    {
        get { return m_instance; }
    }

    [SerializeField] GameObject m_enemy;
    [SerializeField] AudioClip m_attack;

    private Animator m_animator;
    private EnemyController m_controller;

    private void Awake()
    {
        m_instance = this;

        m_animator = GetComponent<Animator>();
        m_controller = m_enemy.GetComponent<EnemyController>();
    }

    public void Run(bool isMove)
    {
        m_animator.SetBool("Run", isMove);
    }

    public void Attack()
    {
        m_animator.SetTrigger("Attack");

        //攻撃エフェクト
    }
    public void AttackEnd()
    {
        //盤面のエフェクトを表示
        TileGrid.PlayEffect();

        //効果音
        SoundManager.Play2D(m_attack);

        //次の攻撃アニメーション
        NextAttack();
    }

    public void NextAttack()
    {
        //アニメーションの再生
        m_animator.SetTrigger("NextAttack");

        //次の攻撃マスをランダムで選択
        TileGrid.RandomSelect();
    }
    public void NextAttackEnd()
    {
        m_controller.IsTurnEnd = true;
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
}