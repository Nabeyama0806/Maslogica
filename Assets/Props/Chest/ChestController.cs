using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PortalGateController;

public class ChestController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        //効果音
        //SoundManager.Play2D(m_gateData.se);

        //エフェクトの表示
        //m_gateData.effect.SetActive(true);

        //シーン遷移
        SceneController.Transition("Select");
    }
}
