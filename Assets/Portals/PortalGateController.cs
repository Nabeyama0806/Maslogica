using System;
using UnityEngine;

public class PortalGateController : MonoBehaviour
{
    public enum GateType
    { 
        Battle,
        AddCard,
        Shop,
    }

    [Serializable]
    public class GateData
    {
        public GateType gateType;
        public GameObject effect;
        public AudioClip se;
    }

    [SerializeField] GateData m_gateData;

    private void Start()
    {
        //m_gateData.effect.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("!!!!!!!!!");

        //効果音
        //SoundManager.Play2D(m_gateData.se);

        //エフェクトの表示
        //m_gateData.effect.SetActive(true);

        //シーン遷移
        switch (m_gateData.gateType)
        {
            case GateType.Battle:
                SceneController.Transition("Battle");
                break;

            case GateType.AddCard:
                SceneController.Transition("AddCard");
                break;

            case GateType.Shop:
                SceneController.Transition("Shop");
                break;
        }
    }
}
