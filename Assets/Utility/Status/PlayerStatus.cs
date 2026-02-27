using UnityEngine;

public class PlayerStatus : CharacterStatus
{
    [SerializeField] PlayerStatusData m_data;

    private VitalStatus m_moveTime = new VitalStatus();

    public VitalStatus MoveTime => m_moveTime;

    protected override void Init()
    {
        // Šî‘b’lİ’è
        m_characterName = m_data.characterName;
        m_moveTime.MaxValue = m_data.moveTime;
        m_health.MaxValue = m_data.health;
        m_power.Value = m_data.power;
        m_defense.Value = m_data.defense;

        // Œ»İ’l‰Šú‰»
        m_moveTime.Init();
        m_health.Init();
    }

    public void Heal(float value)
    {
        m_health.Heal(value);
    }

    public void Clear()
    {
        m_power.Clear();
        m_defense.Clear();
    }
}
