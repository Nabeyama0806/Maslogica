using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    [SerializeField] EffectData m_data;

    public EffectData Data => m_data;

    List<GameObject> m_pool = new();

    GameObject loopInstance;

    void Awake()
    {
        if (m_data.loop)
        {
            loopInstance = CreateInstance();
            loopInstance.transform.SetParent(transform);
            loopInstance.SetActive(false);
        }
    }

    private GameObject CreateInstance()
    {
        GameObject obj = Instantiate(m_data.prefab);
        obj.SetActive(false);
        return obj;
    }

    private GameObject GetPool()
    {
        foreach (var obj in m_pool)
        {
            if (!obj.activeSelf) return obj;
        }

        GameObject newObj = CreateInstance();
        m_pool.Add(newObj);
        return newObj;
    }

    public void Play(Vector3 pos)
    {
        if (m_data.loop)
        {
            loopInstance.transform.position = pos;
            loopInstance.SetActive(true);
        }
        else
        {
            GameObject obj = m_data.usePool ? GetPool() : CreateInstance();

            obj.transform.position = pos;
            obj.SetActive(true);

            StartCoroutine(Stop(obj));
        }

        if (m_data.sound)
        {
            SoundManager.Play2D(m_data.sound, m_data.volume);
        }
    }

    IEnumerator Stop(GameObject obj)
    {
        yield return new WaitForSeconds(m_data.duration);
        obj.SetActive(false);
    }

    public void Stop()
    {
        if (loopInstance)
        {
            loopInstance.SetActive(false);
        }
    }
}