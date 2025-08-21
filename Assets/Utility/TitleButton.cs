using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    [SerializeField] GameObject m_titleBgm;
    [SerializeField] StatusData m_playerStatus;
    [SerializeField] AudioClip m_se;

    public void OnClick()
    {
        //タイトルBGMを停止
        m_titleBgm.SetActive(false);

        //効果音を再生
        SoundManager.Play2D(m_se);

        //シーン遷移
        Fade.FadeOut(1.0f, () =>
        {
            //シーンの破棄
            SceneController.UnLoad("Title");

            //シーンの読み込み
            SceneController.Load("Player");
            SceneController.Load("Select");

            //フェードイン
            Fade.FadeIn(1.0f);
        });
    }
}