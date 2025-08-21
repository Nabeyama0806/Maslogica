using UnityEngine;
using UnityEngine.Events;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] StatusData m_status;
    [SerializeField] UnityEvent m_onDeath;
    [SerializeField] UnityEvent m_onDamage;

    public StatusData Value
    {
        get => m_status;
    }

    public int MaxHealth
    {
        get => m_status.baseHealth;
        set => m_status.baseHealth = value;
    }
    public int CurrentHealth
    {
        get => m_status.currentHealth;
        set => m_status.currentHealth = value;
    }

    public int Power
    {
        get => m_status.currentPower;
        set => m_status.currentPower = value;
    }

    public int Defense
    {
        get => m_status.currentDefense;
        set => m_status.currentDefense = value;
    }

    private void Start()
    {
        //ステータスの初期化
        m_status.Init();
    }

    public void Damage(int power)
    {
        //ダメージ計算(乱数)
        int damage = (power - m_status.currentDefense);

        //マイナスのダメージは与えない
        if (damage <= 0) return;

        //体力がマイナスならダメージを与えない
        if (m_status.currentHealth <= 0) return;

        //ダメージ
        m_status.currentHealth -= damage;

        //体力の確認
        if (m_status.currentHealth <= 0)
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