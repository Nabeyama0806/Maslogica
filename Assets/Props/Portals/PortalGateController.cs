using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class PortalGateController : MonoBehaviour
{
    public enum GateType
    {
        None,          
        Battle,
        AddCard,
        Shop,
    }

    [Serializable]
    public class GateData
    {
        public GateType gateType;       //ゲートの種類
        public GameObject effect;       
    }

    [SerializeField] GateType m_gateType;
    [SerializeField] string m_sceneName;
    [SerializeField] int m_spawnProbability; 
    [SerializeField] List<GateData> m_gateDataList;
    [SerializeField] GameObject m_gateInEffect; 
    [SerializeField] AudioClip m_se;

    private void Start()
    {
        //ランダムでゲートの種類を決定
        int rand = UnityEngine.Random.Range(0, 100);
        if (rand > m_spawnProbability) m_gateType = GateType.Battle;
        m_gateDataList[(int)m_gateType].effect.SetActive(true);
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
        switch (m_gateType)
        {
            case GateType.None:
                SceneController.Transition(m_sceneName, "Select");
                break;

            case GateType.Battle:
                SceneController.Transition(m_sceneName, "Battle");
                break;

            case GateType.AddCard:
                SceneController.Transition(m_sceneName, "AddCard");
                break;

            case GateType.Shop:
                SceneController.Transition(m_sceneName, "Shop");
                break;
        }
    }
}