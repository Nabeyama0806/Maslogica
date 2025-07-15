using System.Diagnostics;
using UnityEngine.SceneManagement;

public class SceneController
{
    static private bool m_isTransition;

    //シーンの追加
    static public void Load(string sceneName, float transitionTime = 1.0f)
    {
        //既に遷移中なら受け付けない
        if (m_isTransition) return;

        //既に追加済みなら何もしない
        if (SceneManager.GetSceneByName(sceneName).isLoaded) return;

        //シーン遷移開始
        m_isTransition = true;

        //フェードアウト
        Fade.FadeOut(transitionTime, () =>
        {
            //追加読み込み
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

            //シーン遷移完了
            m_isTransition = false;

            //フェードイン
            Fade.FadeIn(transitionTime);
        });
    }

    //シーンの除外
    static public void UnLoad(string sceneName, float transitionTime = 1.0f)
    {
        //既に遷移中なら受け付けない
        if (m_isTransition) return;

        //シーン遷移開始
        m_isTransition = true;

        //フェードアウト
        Fade.FadeOut(transitionTime, () =>
        {
            //除外
            SceneManager.UnloadSceneAsync(sceneName);

            //シーン遷移完了
            m_isTransition = false;

            //フェードイン
            Fade.FadeIn(transitionTime);
        });
    }

    //シーン遷移
    static public void Transition(string nextSceneName, float transitionTime = 1.0f)
    {
        //既に遷移中なら受け付けない
        if (m_isTransition) return;

        //シーン遷移開始
        m_isTransition = true;

        //フェードアウト
        Fade.FadeOut(transitionTime, () => 
        {
            //シーン読み込み
            SceneManager.LoadScene(nextSceneName);

            //シーン遷移完了
            m_isTransition = false;

            //フェードイン
            Fade.FadeIn(transitionTime);
        });
    }
}