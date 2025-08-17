using UnityEngine;

public class TitleButton : MonoBehaviour
{
    [SerializeField] GameObject m_titleBgm;
    [SerializeField] AudioClip m_se;

    public void OnClick()
    {
        //タイトルBGMを停止
        m_titleBgm.SetActive(false);

        //これまで保存していたプレイヤーの体力データを削除
        PlayerPrefs.DeleteKey("PlayerHealth");

        SoundManager.Play2D(m_se);
        SceneController.Transition("Select");
    }
}