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
        if (damage <= 0) return;    //���̃_���[�W�͉񕜂��Ă��܂�
        if (m_health <= 0) return;  //���̏R��͂��Ȃ�

        //�_���[�W
        m_health -= damage;

        //�̗͂̊m�F
        if (m_health <= 0)
        {
            //���S�ʒm
            m_onDeath?.Invoke();
        }
        else
        {
            //��e�ʒm
            m_onDamage?.Invoke();
        }
    }
}