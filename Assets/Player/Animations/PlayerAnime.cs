using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    [SerializeField] GameObject m_effect;
    [SerializeField] AudioClip m_attack;

    static GameObject m_auraEffect;
    static private Animator m_animator;
    static private PlayerController m_controller;

    private void Start()
    {
        m_auraEffect = m_effect;
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

        //エフェクトを表示
        m_auraEffect.SetActive(true);
    }
    public void AttackEnd()
    {
        //ターン終了
        m_auraEffect.SetActive(false);
        m_controller.GetComponent<PlayerController>().IsTurnEndFlag = true;

        //盤面のエフェクトを表示
        TileGrid.PlayEffect();

        //効果音
        SoundManager.Play2D(m_attack);
    }
}
