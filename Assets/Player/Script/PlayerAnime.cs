using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] AudioClip m_attack;

    static private Animator m_animator;
    static private PlayerController m_controller;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
        m_controller = m_player.GetComponent<PlayerController>();
    }

    static public void Run(bool isMove)
    {
        m_animator.SetBool("Run", isMove);
    }

    static public void Attack()
    {
        m_animator.SetTrigger("Attack");

        //攻撃エフェクト
        PlayerEffects.Instance.Play(PlayerEffects.EffectType.Aura);
    }
    public void AttackEnd()
    {
        //盤面のエフェクトを表示
        TileGrid.PlayEffect();

        //効果音
        SoundManager.Play2D(m_attack);

        //攻撃エフェクト
        PlayerEffects.Instance.Stop(PlayerEffects.EffectType.Aura);

        //ターン終了
        m_controller.GetComponent<PlayerController>().IsTurnEndFlag = true;
    }

    static public void Death()
    {
        m_animator.SetBool("Death", true);
    }
    public void DeathEnd()
    {
        //シーン遷移
        SceneController.Transition("Game");
    }
}