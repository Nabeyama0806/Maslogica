using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialSceneController : MonoBehaviour
{
    [SerializeField] InputAction m_clickAction;
    [SerializeField] List<string> m_texts;
    [SerializeField] TextController m_textController;
    [SerializeField] TextMesh m_textMesh;
    [SerializeField] AudioClip m_se;

    private string m_playerName;

    public string PlayerName
    {
        get { return m_playerName; }
        set { m_playerName = value; }
    }

    private void Start()
    {
        m_textController.Initialize(m_texts);
    }

    private void Awake()
    {
        m_clickAction.performed += ctx => OnClick();
    }

    private void OnEnable()
    {
        m_clickAction.Enable();
    }

    private void OnDisable()
    {
        m_clickAction.Disable();
    }

    public void OnClick()
    {
        SoundManager.Play2D(m_se);

        //テキスト表示が完了したらシーン遷移
        if (!m_textController.Advance())
        {
            BGM.Instance.Stop();

            Fade.FadeOut(1.0f, () =>
            {
                SceneController.UnLoad(SceneType.Tutorial);
                SceneController.Load(SceneType.Player);
                SceneController.Load(SceneType.Select);

                BGM.Instance.Play(SceneType.Select);
                Fade.FadeIn(1.0f);
            });
        }
    }
}