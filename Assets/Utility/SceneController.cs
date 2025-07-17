using UnityEngine.SceneManagement;

public class SceneController
{
    static private bool m_isTransition = false;

    //シーン遷移
    static public void Transition(string sceneName)
    {
        //既に遷移中なら受け付けない
        if (m_isTransition) return;

        //既に追加済みなら何もしない
        if (SceneManager.GetSceneByName(sceneName).isLoaded) return;

        //シーン遷移開始
        m_isTransition = true;

        //フェードアウト
        Fade.FadeOut(1.0f, () =>
        {
            //次のシーンを読み込む
            SceneManager.LoadScene(sceneName);

            //シーン遷移完了
            m_isTransition = false;

            //フェードイン
            Fade.FadeIn(1.0f);
        });
    }
}