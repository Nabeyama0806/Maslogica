using System.Collections;
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
    [SerializeField] GameObject m_model;

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

    public void Damage()
    {
        m_animator.SetTrigger("Damage");

        //ダメージエフェクト
       // StartCoroutine(PlayerEffects.Instance.AutoPlay(PlayerEffects.EffectType.Damage));
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

        //プレイヤーの移動を停止
        m_player.GetComponent<PlayerController>().enabled = false;
    }

    public void DeathEnd()
    {
        //シーンの破棄
        SceneController.Transition(SceneController.Type.Battle, SceneController.Type.Result);
    }

    public void Hidden()
    {
        StartCoroutine(_Hidden());
    }
    private IEnumerator _Hidden()
    {
        //プレイヤーのモデルを非表示にする
        m_model.SetActive(false);

        //待機
        yield return new WaitForSeconds(1.0f);

        //プレイヤーのモデルを表示する
        m_model.SetActive(true);
    }

}