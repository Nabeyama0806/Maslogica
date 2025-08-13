using UnityEngine;
using UnityEngine.Events;

public class CharacterStatus : MonoBehaviour
{
    [SerializeField] CharacterData m_characterData;
    [SerializeField] UnityEvent m_onDeath;
    [SerializeField] UnityEvent m_onDamage;
    
    private int m_health;

    public CharacterData Value
    {
        get => m_characterData;
    }

    public int Health
    {
        get => m_health;
    }

    private void Awake()
    {
        m_health = m_characterData.MaxHealth;
    }

    public void Damage(int power)
    {
        //ダメージ計算(乱数)
        int damage = (power - m_characterData.Defense); 

        if (damage <= 0) return;     //負のダメージは回復してしまう
        if (m_health <= 0) return;   //死体蹴りはしない

        //ダメージ
        m_health -= damage;

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