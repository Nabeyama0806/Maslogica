using System;
using UnityEngine;
using System.Collections.Generic;

public class BGM : MonoBehaviour
{
    static private BGM m_instance;

    static public BGM Instance
    {
        get { return m_instance; }
    }


    [Serializable]
    public class SoundData
    {
        public SceneController.Type type; 
        public AudioClip sound;
        public float volume; 
    }

    [SerializeField] List<SoundData> m_soundList; 

    private Dictionary<SceneController.Type, AudioClip> m_soundDataList;
    private AudioSource m_audioSource;

    private void Awake()
    {
        m_instance = this;
        m_audioSource = GetComponent<AudioSource>();
        m_soundDataList = new Dictionary<SceneController.Type, AudioClip>();

        //BGMとステージを紐づけるための連想配列リストを作成
        foreach (var sound in m_soundList)
        {
            m_soundDataList.Add(sound.type, sound.sound);
        }
    }

    public void Play(SceneController.Type type)
    {
        //BGMが登録されていなければ何もしない
        if (m_soundDataList[type] == null) return;

        //BGMの付け替え
        m_audioSource.clip = m_soundDataList[type];

        //音量の調整
        m_audioSource.volume = m_soundList[(int)type].volume;

        m_audioSource.Play();
    }

    public void Stop()
    {
        m_audioSource.Stop();
    }
}