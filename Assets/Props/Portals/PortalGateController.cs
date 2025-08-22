using System;
using UnityEngine;
using System.Collections.Generic;

public class PortalGateController : MonoBehaviour
{
    [Serializable]
    public class GateEffectData
    {
        public SceneController.Type gateType;     
        public GameObject effect;       
    }

    [SerializeField] SceneController.Type m_scene;
    [SerializeField] SceneController.Type m_nextScene;
    [SerializeField] int m_spawnProbability;        
    [SerializeField] GameObject m_gateInEffect; 
    [SerializeField] AudioClip m_se;
    [SerializeField] List<GateEffectData> m_gateEffectDataList;

    private Dictionary<SceneController.Type, GameObject> m_gateEffects;

    private void Start()
    {
        //ランダムでゲートの種類を決定
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand > m_spawnProbability) m_nextScene = SceneController.Type.Battle;

        //ゲートエフェクトとステージを紐づけるための連想配列リストを作成
        m_gateEffects = new Dictionary<SceneController.Type, GameObject>();
        foreach (var effectDate in m_gateEffectDataList)
        {
            m_gateEffects.Add(effectDate.gateType, effectDate.effect);
        }

        //指定したゲートエフェクトを表示
        m_gateEffects[m_nextScene].SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        //プレイヤー以外は何もしない
        if (!other.CompareTag("Player")) return;

        //エフェクトの再生
        m_gateInEffect.SetActive(true);

        //効果音
        SoundManager.Play2D(m_se, 0.5f);

        //シーン遷移
        SceneController.Transition(m_scene, m_nextScene);
    }
}