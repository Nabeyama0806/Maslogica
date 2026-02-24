using UnityEngine;
using UnityEngine.Events;

public class CharacterStatus : MonoBehaviour
{
    private const float MinDamageRate = 0.8f;
    private const float MaxDamageRate = 1.2f;

    [SerializeField] StatusParameter m_statusData;
    [SerializeField] UnityEvent m_onDeath;
    [SerializeField] UnityEvent m_onDamage;
    
    private int m_health;

    public StatusParameter Base => m_statusData;

    public int CurrentHealth
    {
        get => m_health;
        set => m_health = Mathf.Clamp(value, 0, m_statusData.maxHealth);
    }

    private void Start()
    {
        //体力の初期化
        m_health = m_statusData.maxHealth;
    }

    public void Damage(int power)
    {
        //基本ダメージ計算
        int baseDamage = (power * 2) - (m_statusData.defense / 4);

        //乱数
        float randomFactor = Random.Range(MinDamageRate, MaxDamageRate);

        //乱数を考慮したダメージ
        int damage = Mathf.RoundToInt(baseDamage * randomFactor);
        
        //マイナスのダメージは与えない
        if (damage <= 0) return;

        //体力がマイナスならダメージを与えない
        if (m_health <= 0) return;

        //ダメージ
        m_health -= damage;
        Debug.Log("ダメージ量 : " + damage);

        //体力の確認
        if (m_health <= 0)
        {
            //死亡通知
            m_onDeath?.Invoke();
        }
        else
        {
            //被弾通知
            m_onDamage?.Invoke();
        }
    }
}