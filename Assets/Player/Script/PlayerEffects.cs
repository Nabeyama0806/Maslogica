using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour
{
    static private PlayerEffects m_instance;

    static public PlayerEffects Instance
    { 
        get { return m_instance; }
    }

    private void Awake()
    {
        m_instance = this;
    }

    //Typeとエフェクトを紐づけるためのクラス
    [Serializable]
    public class Effects            
    {
        public EffectType type;     //自身がどのエフェクトか
        public GameObject effect;
        public float playTime;      //再生する時間  
    }

    [SerializeField] List<Effects> m_effectList;

    public enum EffectType
    {
        Snup,   
        Aura,   
        Damage,
        Death,
    }

    //エフェクトの表示
    public IEnumerator AutoPlay(EffectType type)
    {
        //再生
        Play(type);

        //待機
        yield return new WaitForSeconds(m_effectList[(int)type].playTime);

        //停止
        Stop(type);
    }

    public void Play(EffectType type)
    {
        //再生
        m_effectList[(int)type].effect.SetActive(true);
    }

    //全てのエフェクトを非表示にする
    public void AllStop()
    {
        foreach (Effects type in m_effectList)
        {
            type.effect.SetActive(false);
        }
    }

    //エフェクトを非表示にする
    public void Stop(EffectType type)
    {
        m_effectList[(int)type].effect.SetActive(false);
    }
}
