using UnityEngine;

[CreateAssetMenu(menuName = "StatusData")]
public class CharacterData : ScriptableObject
{
    public string Name; 
    public int MaxHealth; 
    public int Power; 
    public int Defense; 
    public int Gold;
}