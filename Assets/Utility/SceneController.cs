using UnityEngine.SceneManagement;

public class SceneController
{
   static private bool m_isTransition;

    static public void Load(string sceneName)
    {
        //既に追加済みなら何もしない
        if (!SceneManager.GetSceneByName(sceneName).isLoaded) return;
        
        //追加読み込み
        SceneManager.LoadScene(sceneName, LoadSceneMode.Additive);
    }

    static public void UnLoad(string sceneName)
    { 
        SceneManager.UnloadSceneAsync(sceneName);
    }

    static public void Change(string nextSceneName, string prevSceneName)
    {
        Load(nextSceneName);

        UnLoad(prevSceneName);
    }

    //シーン遷移
    static public void Transition(string nextSceneName)
    {
        //既に遷移中なら受け付けない
        if (m_isTransition) return;

        //シーン遷移開始
        m_isTransition = true;

        //フェードアウト
        Fade.FadeOut(1.0f, () => {

            //シーン読み込み
            SceneManager.LoadScene(nextSceneName);

            //シーン遷移完了
            m_isTransition = false;

            //フェードイン
            Fade.FadeIn(1.0f);
        });
    }
}
