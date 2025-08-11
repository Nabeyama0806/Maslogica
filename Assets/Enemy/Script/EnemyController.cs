using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool m_isAttack;
    private bool m_isTurnEnd;

    public bool IsAttack
    {
        set { m_isAttack = value; }
    }
    public bool IsTurnEnd
    {
        set { m_isTurnEnd = value; }
    }

    private void Start()
    {
        m_isAttack = false;
        m_isTurnEnd = false;
    }

    public bool Play()
    {
        //攻撃
        EnemyAnime.Instance.Attack();
        
        return m_isTurnEnd;
    }

    public void OnDeath()
    {
        //死亡アニメーション
        EnemyAnime.Instance.Death();
    }
}
