using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneType
{
    Title,
    Player,
    Select,
    Battle,
    AddCard,
    Shop,
    Result,
    System,
}

public class SceneController
{
    static private bool m_isTransition = false;

    //シーンを初期状態で読み込む
    static public void Redo(SceneType scene)
    {
        //既に遷移中なら受け付けない
        if (m_isTransition) return;

        //シーンの読み込み
        SceneManager.LoadScene(SceneName(scene));
    }

    //シーンの追加
    static public void Load(SceneType scene)
    {
        //既に遷移中なら受け付けない
        if (m_isTransition) return;

        //既に追加済みなら何もしない
        if (SceneManager.GetSceneByName(SceneName(scene)).isLoaded) return;

        //シーンの追加読み込み
        SceneManager.LoadScene(SceneName(scene), LoadSceneMode.Additive);
    }

    //シーンの除外
    static public void UnLoad(SceneType scene)
    {
        //既に遷移中なら受け付けない
        if (m_isTransition) return;

        //シーンの除外
        SceneManager.UnloadSceneAsync(SceneName(scene));
    }

    //シーン遷移
    static public void Transition(SceneType prevScene, SceneType nextScene)
    {
        //既に遷移中なら受け付けない
        if (m_isTransition) return;

        //既に追加済みなら何もしない
        if (SceneManager.GetSceneByName(SceneName(nextScene)).isLoaded) return;

        //シーン遷移開始
        m_isTransition = true;

        //前のシーンのBGMを停止
        BGM.Instance.Stop();

        //フェードアウト
        Fade.FadeOut(1.0f, () =>
        {
            //次のシーンを読み込む
            SceneManager.LoadScene(SceneName(nextScene), LoadSceneMode.Additive);

            //前のシーンを除外する
            SceneManager.UnloadSceneAsync(SceneName(prevScene));

            //シーン遷移完了
            m_isTransition = false;

            //シーンのBGMを再生
            BGM.Instance.Play(nextScene);

            //フェードイン
            Fade.FadeIn(1.0f);
        });
    }

    static private string SceneName(SceneType type)
    {
        switch (type)
        { 
            case SceneType.Title:
                return "Title";

            case SceneType.Player:
                return "Player";

            case SceneType.Select:
                return "Select";

            case SceneType.Battle:
                return "Battle";

            case SceneType.AddCard:
                return "AddCard";

            case SceneType.Shop:
                return "Shop";

            case SceneType.Result:
                return "Result";

            case SceneType.System:
                return "System";

            default:
                Debug.Log("シーンが存在しません。");
                return "Unknown";
        }
    }
}