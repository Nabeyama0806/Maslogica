using UnityEngine;

public class TitleButton : MonoBehaviour
{
    [SerializeField] AudioClip m_se;

    private void Awake()
    {
        // タイトルシーンのBGMを再生
        BGM.Instance.Play(SceneController.Type.Title);
    }

    public void OnClick()
    {
        //BGMの停止
        BGM.Instance.Stop();

        //効果音を再生
        SoundManager.Play2D(m_se);

        //シーン遷移
        Fade.FadeOut(1.0f, () =>
        {
            //シーンの破棄
            SceneController.UnLoad(SceneController.Type.Title);

            //シーンの読み込み
            SceneController.Load(SceneController.Type.Player);
            SceneController.Load(SceneController.Type.Select);

            //ステージ選択シーンのBGMを再生
            BGM.Instance.Play(SceneController.Type.Select);

            //フェードイン
            Fade.FadeIn(1.0f);
        });
    }
}