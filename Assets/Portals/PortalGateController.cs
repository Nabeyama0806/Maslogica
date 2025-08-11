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

    [SerializeField] GateType m_gateType;
    [SerializeField] AudioClip m_se;

    private void Start()
    {
        //バトルゲート以外は、ランダム出現にする
        if (m_gateType == GateType.Battle) return;

        //20%の確率で出現
        if (UnityEngine.Random.Range(0, 100) < 20) 
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        //効果音
        //SoundManager.Play2D(m_se);

        //シーン遷移
        switch (m_gateType)
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
