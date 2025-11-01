using UnityEngine;
using System;

[CreateAssetMenu(menuName = "CharacterStatusData")]
public class StatusData : ScriptableObject
{
    //最低限のステータス
    [Header("初期ステータス")]
    public int baseHealth;
    public int basePower;
    public int baseDefense;
    public float baseMoveTime;

    //装備やバフで変動するステータス
    [Header("実行時の変動ステータス")]
    [NonSerialized] public int currentHealth;
    [NonSerialized] public int currentPower;   
    [NonSerialized] public int currentDefense;
    [NonSerialized] public float currentMoveTime;

    public void Init()
    {
        currentHealth = baseHealth;
        currentPower = basePower;
        currentDefense = baseDefense;
        currentMoveTime = baseMoveTime;
    }

    public void StatusReset()
    {
        currentPower = basePower;
        currentDefense = baseDefense;
    }

    public void FullRecovery()
    {
        currentHealth = baseHealth;
    }
}