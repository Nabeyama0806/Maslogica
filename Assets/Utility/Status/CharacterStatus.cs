using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;

public abstract class CharacterStatus : MonoBehaviour
{
    private const float MinDamageRate = 0.9f;
    private const float MaxDamageRate = 1.1f;
    private const int PowerRate = 2;
    private const int DefenseRate = 4;

    [SerializeField] UnityEvent m_onDeath;
    [SerializeField] UnityEvent m_onDamage;

    protected VitalStatus m_health = new VitalStatus();
    protected CombatStatus m_power = new CombatStatus();
    protected CombatStatus m_defense = new CombatStatus();

    public VitalStatus Health => m_health;

    public CombatStatus Power => m_power;

    public CombatStatus Defense => m_defense;

    private void Start()
    {
        Init();
    }

    protected abstract void Init();

    public void Damage(CharacterStatus status)
    {
        if (m_health.CurrentTotal <= 0) return;

        //基本ダメージ
        int baseDamage = (status.Power.TotalValue * PowerRate) - (m_defense.TotalValue / DefenseRate);

        //最低1ダメージ保証
        if (baseDamage <= 0) baseDamage = 1;

        //乱数
        float randomFactor = Random.Range(MinDamageRate, MaxDamageRate);

        //最終ダメージ
        int damage = Mathf.RoundToInt(baseDamage * randomFactor);

        //ダメージ適用
        m_health.Damage(damage);

        Debug.Log(name + " が受けたダメージ : " + damage);

        // 死亡判定
        if (m_health.CurrentTotal <= 0)
        {
            m_onDeath?.Invoke();
        }
        else
        {
            m_onDamage?.Invoke();
        }
    }
}