using System.Diagnostics;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class SceneController
{
    static private bool m_isTransition = false;

    //シーンの追加
    static public void Load(string sceneName)
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
            //シーンの追加読み込み
            SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);

            //シーン遷移完了
            m_isTransition = false;

            //フェードイン
            Fade.FadeIn(1.0f);
        });
    }

    //シーンの除外
    static public void UnLoad(string sceneName)
    {
        //既に遷移中なら受け付けない
        if (m_isTransition) return;

        //シーン遷移開始
        m_isTransition = true;

        //フェードアウト
        Fade.FadeOut(1.0f, () =>
        {
            //シーンの除外
            SceneManager.UnloadSceneAsync(sceneName);

            //シーン遷移完了
            m_isTransition = false;

            //フェードイン
            Fade.FadeIn(1.0f);
        });
    }

    //シーン遷移
    static public void Transition(string prevSceneName, string nextSceneName)
    {
        //既に遷移中なら受け付けない
        if (m_isTransition) return;

        //既に追加済みなら何もしない
        if (SceneManager.GetSceneByName(nextSceneName).isLoaded) return;

        //シーン遷移開始
        m_isTransition = true;

        //フェードアウト
        Fade.FadeOut(1.0f, () =>
        {
            //次のシーンを読み込む
            SceneManager.LoadScene(nextSceneName, LoadSceneMode.Additive);

            //前のシーンを除外する
            SceneManager.UnloadSceneAsync(prevSceneName);

            //シーン遷移完了
            m_isTransition = false;

            //フェードイン
            Fade.FadeIn(1.0f);
        });
    }
}