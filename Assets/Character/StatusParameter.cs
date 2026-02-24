using UnityEngine;
using System;
using UnityEditorInternal;

[CreateAssetMenu(menuName = "StatusParameter")]
public class StatusParameter : ScriptableObject
{
    public int maxHealth;
    public int power;
    public int defense;
    public float moveTime;

    public StatusParameter Clone()
    {
        return (StatusParameter)MemberwiseClone();
    }

    static public StatusParameter operator +(StatusParameter a, StatusParameter b)
    {
        StatusParameter result = a.Clone();
        result.maxHealth += b.maxHealth;
        result.power += b.power;
        result.defense += b.defense;
        result.moveTime += b.moveTime;
        return result;
    }

    static public StatusParameter operator -(StatusParameter a, StatusParameter b)
    {
        StatusParameter result = a.Clone();
        result.maxHealth -= b.maxHealth;
        result.power -= b.power;
        result.defense -= b.defense;
        result.moveTime -= b.moveTime;
        return result;
    }
}