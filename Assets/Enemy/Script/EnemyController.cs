using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private bool m_isTurnEnd;
    public bool IsTurnEndFlag
    {
        set { m_isTurnEnd = value; }
    }

    private void Start()
    {
        m_isTurnEnd = false;
    }

    public bool IsTurnEnd()
    {
        //攻撃
        EnemyAnime.Instance.Attack();

        //ターン終了
        return m_isTurnEnd;
    }

    public void OnDeath()
    {
        //死亡アニメーション
        EnemyAnime.Instance.Death();
    }

    public void OnDamage()
    { 
        EnemyAnime.Instance.Damage();
    }
}
