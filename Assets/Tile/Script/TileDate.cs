using UnityEngine;
using static TileEffects;

public class TileDate : MonoBehaviour
{
    [SerializeField] GameObject m_frame;            //タイルの淵
    [SerializeField] Material m_damageMaterial;     //エネミーの攻撃マスのマテリアル
    [SerializeField] AudioClip m_active;            //アクティブ時の効果音

    private TileEffects m_effects;  //エフェクト管理クラス
    private bool m_isActive;        //アクティブかどうか
    private bool m_isEnemyAttack;   //エネミーの攻撃マスかどうか

    public bool IsActive
    {
        get { return m_isActive; }
        set { m_isActive = value; }
    }

    public bool IsEnemyAttack
    {
        get { return m_isEnemyAttack; }
        set { m_isEnemyAttack = value; }
    }

    private void Start()
    {        
        //状態のリセット
        m_effects = GetComponent<TileEffects>();
        Inactive();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //状態の反転
            m_isActive = !m_isActive;

            //エフェクト
            m_effects.Show(EffectType.Active, m_isActive);

            //効果音
            SoundManager.Play2D(m_active, 0.4f);

            //エネミーの攻撃マスならダメージを与える
            if (m_isEnemyAttack)
            {
                other.GetComponent<Health>().Damage(20);
            }
        }
    }

    //非アクティブにする
    public void Inactive()
    {
        m_isActive = false;
        m_effects.Inactive();
    }

    //エネミーの攻撃マス
    public void EnemyAttack()
    {
        m_isEnemyAttack = true;
        m_frame.GetComponent<MeshRenderer>().material = m_damageMaterial;
        m_effects.Show(EffectType.EnemyAttack);
    }
}