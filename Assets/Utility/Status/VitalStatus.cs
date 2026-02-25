using System;

[Serializable]
public class VitalStatus
{
    //Å‘å’l
    private float maxValue;
    private float maxAddValue;

    //Œ»Ý’l
    private float currentValue;
    private float currentAddValue;

    public float MaxTotal => maxValue + maxAddValue;

    public float CurrentTotal => currentValue + currentAddValue;

    public float Normalized => MaxTotal <= 0 ? 0 : CurrentTotal / MaxTotal;

    public float MaxValue
    {
        get => maxValue;
        set
        {
            maxValue = value;
            Clamp();
        }
    }

    public float CurrentValue
    {
        get => currentValue;
        set
        {
            currentValue = value;
            Clamp();
        }
    }
    public void Init()
    {
        maxAddValue = 0f;
        currentAddValue = 0f;
        currentValue = maxValue;
    }

    public void AddMax(float value)
    {
        maxAddValue += value;
        Clamp();
    }

    public void AddCurrent(float value)
    {
        currentAddValue += value;
        Clamp();
    }

    public void Damage(float value)
    {
        currentValue -= value;
        Clamp();
    }

    public void Heal(float value)
    {
        currentValue += value;
        Clamp();
    }

    public void SetZero()
    {
        currentValue = -currentAddValue;
        Clamp();
    }

    public void FullRecover()
    {
        currentValue = MaxTotal - currentAddValue;
        Clamp();
    }

    private void Clamp()
    {
        if (CurrentTotal > MaxTotal) currentValue = MaxTotal - currentAddValue;
        if (CurrentTotal < 0f) currentValue = -currentAddValue;
    }
}