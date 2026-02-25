using UnityEngine;

[System.Serializable]
public class CombatStatus
{
    private int currentValue;
    private int addValue;

    public int Value
    {
        get => currentValue;
        set => currentValue = Mathf.Max(0, value);
    }

    public int AddValue => addValue;

    public int TotalValue => currentValue + addValue;

    public void Add(int value)
    {
        addValue += value;
    }

    public void Remove(int value)
    {
        addValue -= value;
    }

    public void Clear()
    {
        addValue = 0;
    }
}