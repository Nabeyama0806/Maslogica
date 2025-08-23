using UnityEngine;

public class TileDate : MonoBehaviour
{
    [SerializeField] GameObject m_frame;            //タイルの淵
    [SerializeField] Material m_normalMaterial;
    [SerializeField] Material m_damageMaterial;     //エネミーの攻撃マスのマテリアル
    [SerializeField] AudioClip m_active;            
    [SerializeField] AudioClip m_enemyAttack;       //エネミーの攻撃マスの効果音

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
            if (m_isActive) m_effects.Play(TileEffects.EffectType.Active);
            else m_effects.Stop(TileEffects.EffectType.Active);

            //効果音
            SoundManager.Play2D(m_active, 0.6f);

            //エネミーの攻撃マスならダメージを与える
            if (m_isEnemyAttack)
            {
                other.GetComponent<CharacterStatus>().Damage(40);

                //効果音
                SoundManager.Play2D(m_enemyAttack, 0.3f);

                //エフェクト
                StartCoroutine(PlayerEffects.Instance.AutoPlay(PlayerEffects.EffectType.Damage));
            }
        }
    }

    //非アクティブにする
    public void Inactive()
    {
        m_isActive = false;
        m_effects.Stop(TileEffects.EffectType.Active);
    }

    //プレイヤーの攻撃マス
    public void PlayerAttack()
    {
        StartCoroutine(m_effects.AutoPlay(TileEffects.EffectType.PlayerAttack));
    }

    //エネミーの攻撃マス
    public void EnemyAttack()
    {
        m_isEnemyAttack = true;
        m_frame.GetComponent<MeshRenderer>().material = m_damageMaterial;
        m_effects.Play(TileEffects.EffectType.EnemyAttack);
    }

    public void Close()
    {
        //状態のリセット
        m_isActive = false;
        m_isEnemyAttack = false;

        //エフェクトの停止
        m_effects.Stop(TileEffects.EffectType.Active);
        m_effects.Stop(TileEffects.EffectType.EnemyAttack);

        // 元のマテリアルに戻す
        m_frame.GetComponent<MeshRenderer>().material = m_normalMaterial; 
    }
}