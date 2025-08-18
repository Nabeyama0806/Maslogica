using UnityEngine;

public class PlayerAnime : MonoBehaviour
{
    static private PlayerAnime m_instance;

    static public PlayerAnime Instance
    {
        get { return m_instance; }
    }

    [SerializeField] GameObject m_player;
    [SerializeField] AudioClip m_attack;
    [SerializeField] AudioClip m_death;
    [SerializeField] GameObject m_gameBgm;

    private Animator m_animator;
    private PlayerController m_controller;

    private void Awake()
    {
        m_instance = this;

        m_animator = GetComponent<Animator>();
        m_controller = m_player.GetComponent<PlayerController>();
    }

    public void Run(bool isMove)
    {
        m_animator.SetBool("Run", isMove);
    }

    public void Attack()
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
        m_controller.IsTurnEndFlag = true;
    }

    public void Open()
    {
        m_animator.SetTrigger("Open");
    }

    public void Death()
    {
        m_animator.SetBool("Death", true);

        //効果音
        SoundManager.Play2D(m_death);

        //ゲームBGMを停止
        m_gameBgm.SetActive(false);

        //プレイヤーの移動を停止
        m_player.GetComponent<PlayerController>().enabled = false;
        m_player.GetComponent<PlayerMove>().enabled = false;
    }
    public void DeathEnd()
    {
        //シーン遷移
        SceneController.Transition("Title");
    }
}