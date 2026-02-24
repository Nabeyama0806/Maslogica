using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    [SerializeField] List<Effect> m_effects;

    private Dictionary<EffectData, Effect> m_table;

    void Awake()
    {
        m_table = new Dictionary<EffectData, Effect>();

        foreach (var e in m_effects)
        {
            if (!m_table.ContainsKey(e.Data)) m_table.Add(e.Data, e);
        }
    }

    public void Play(EffectData data, Vector3 pos)
    {
        if (m_table.TryGetValue(data, out var effect))
        {
            effect.Play(pos);
        }
    }

    public void Stop(EffectData data)
    {
        if (m_table.TryGetValue(data, out var effect))
        {
            effect.Stop();
        }
    }
}