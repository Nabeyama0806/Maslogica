using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSceneManager : MonoBehaviour
{
    [SerializeField] SceneController.Type m_firstScene;

    void Start()
    {
        //タイトルシーンのBGMを再生
        BGM.Instance.Play(SceneController.Type.Title);

        //タイトルシーンの読み込み
        SceneController.Load(m_firstScene);
    }
}
