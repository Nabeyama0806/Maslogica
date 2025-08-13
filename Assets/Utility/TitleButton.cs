using UnityEngine;

public class TitleButton : MonoBehaviour
{
    [SerializeField] AudioClip m_se;

    public void OnClick()
    {
        //これまで保存していたプレイヤーの体力データを削除
        PlayerPrefs.DeleteKey("PlayerHealth");

        SoundManager.Play2D(m_se);
        SceneController.Transition("Select");
    }
}