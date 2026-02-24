using UnityEngine;
using UnityEngine.Events;

public class CharacterStatus : MonoBehaviour
{
    private const float MinDamageRate = 0.9f;
    private const float MaxDamageRate = 1.1f;
    private const int PowerRate = 2;
    private const int DefenseRate = 4;

    [SerializeField] StatusParameter m_statusData;
    [SerializeField] UnityEvent m_onDeath;
    [SerializeField] UnityEvent m_onDamage;
    
    private StatusParameter m_status;
    private StatusParameter m_buff;

    public StatusParameter Base => m_statusData;

    public StatusParameter Current => m_status;

    public StatusParameter Buff => m_buff;

    private void Start()
    {
        //ステータスの初期化
        m_status = m_statusData.Clone();
        m_buff = new StatusParameter();
    }

    public void Heal(int value)
    {
        //体力の回復
        m_status.health += value;

        //最大体力を超えないようにする
        if (m_status.health > m_statusData.health)
        {
            m_status.health = m_statusData.health;
        }
    }

    public void Damage(CharacterStatus status)
    {
        //バフを考慮した基本ダメージ計算
        StatusParameter other = status.Current + status.Buff;
        int baseDamage = (other.power * PowerRate) - (m_statusData.defense / DefenseRate);

        //乱数
        float randomFactor = Random.Range(MinDamageRate, MaxDamageRate);

        //乱数を考慮したダメージ
        int damage = Mathf.RoundToInt(baseDamage * randomFactor);
        
        //マイナスのダメージは与えない
        if (damage <= 0) return;

        //体力がマイナスならダメージを与えない
        if (m_status.health <= 0) return;

        //ダメージ
        m_status.health -= damage;
        Debug.Log("ダメージ量 : " + damage);

        //体力の確認
        if (m_status.health <= 0)
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