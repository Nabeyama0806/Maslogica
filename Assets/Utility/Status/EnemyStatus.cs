using UnityEngine;

public class EnemyStatus : CharacterStatus
{
    [SerializeField] EnemyStatusData m_data;

    protected override void Init()
    {
        // Šî‘b’lİ’è
        m_characterName = m_data.characterName;
        m_health.MaxValue = m_data.health;
        m_power.Value = m_data.power;
        m_defense.Value = m_data.defense;

        // Œ»İ’l‰Šú‰»
        m_health.Init();
    }
}
