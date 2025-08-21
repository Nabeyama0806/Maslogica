using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemSceneManager : MonoBehaviour
{
    [SerializeField] string m_firstSceneName;

    void Start()
    {
        SceneController.Load(m_firstSceneName);
    }
}
