using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] int m_health;
    [SerializeField] UnityEvent m_onDeath;
    [SerializeField] UnityEvent m_onDamage;

    public int Value
    {
        get => m_health;
    }

    public void Damage(int damage)
    {
        if (damage <= 0) return;    //負のダメージは回復してしまう
        if (m_health <= 0) return;  //死体蹴りはしない

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