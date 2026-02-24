using UnityEngine;
using System;
using UnityEditorInternal;

[CreateAssetMenu(menuName = "StatusParameter")]
public class StatusParameter : ScriptableObject
{
    public int health;
    public int power;
    public int defense;
    public float moveTime;

    public StatusParameter Clone()
    {
        return (StatusParameter)MemberwiseClone();
    }

    public void BuffReset()
    {
        health = 0;
        power = 0;
        defense = 0;
        moveTime = 0f;
    }

    static public StatusParameter operator +(StatusParameter a, StatusParameter b)
    {
        StatusParameter result = a.Clone();
        result.health += b.health;
        result.power += b.power;
        result.defense += b.defense;
        result.moveTime += b.moveTime;
        return result;
    }

    static public StatusParameter operator -(StatusParameter a, StatusParameter b)
    {
        StatusParameter result = a.Clone();
        result.health -= b.health;
        result.power -= b.power;
        result.defense -= b.defense;
        result.moveTime -= b.moveTime;
        return result;
    }
}