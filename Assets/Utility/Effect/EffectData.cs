using UnityEngine;

[CreateAssetMenu(menuName = "EffectData")]
public class EffectData : ScriptableObject
{
    //エフェクト
    public GameObject prefab;
    public float duration = 2f;
    public bool loop = false;
    public bool usePool = true;

    //効果音
    public AudioClip sound;
    public float volume = 1f;
}