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

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        //Œø‰Ê‰¹
        //SoundManager.Play2D(m_se);

        //ƒV[ƒ“‘JˆÚ
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
