using UnityEngine;

public class TileDate : MonoBehaviour
{
    private TileCondition m_condition;  //状態管理クラス
    private bool m_isActive;            //アクティブかどうか

    public bool IsActive => m_isActive;

    private void Start()
    {        
        //状態のリセット
        m_condition = GetComponent<TileCondition>();
        Inactive();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        //状態の反転
        m_isActive = !m_isActive;

        //エフェクト
        m_condition.IsActive(m_isActive);
    }

    //非アクティブにする
    public void Inactive()
    {
        m_isActive = false;
        m_condition.ActiveEffect.SetActive(false);
    }

    public void Close()
    {
        //状態のリセット
        Inactive();

        //エフェクトの停止
        m_condition.AllEffectOff();
    }
}