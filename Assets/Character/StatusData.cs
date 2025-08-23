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

    //装備やバフで変動するステータス
    [Header("実行時の変動ステータス")]
    [NonSerialized] public int currentHealth;
    [NonSerialized] public int currentPower;   
    [NonSerialized] public int currentDefense;

    public void Init()
    {
        currentHealth = baseHealth;
        currentPower = basePower;
        currentDefense = baseDefense;
    }

    public void FullRecovery()
    {
        currentHealth = baseHealth;
    }
}