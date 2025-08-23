using UnityEngine;

public class SystemSceneManager : MonoBehaviour
{
    [SerializeField] SceneController.Type m_firstScene;

    void Start()
    {
        //タイトルシーンの読み込み
        SceneController.Load(m_firstScene);
    }
}
