using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class TutorialSceneController : MonoBehaviour
{
    [SerializeField] TextAnimation m_textAnimation;
    [SerializeField] TextMeshProUGUI m_textObject;
    [SerializeField] List<string> m_texts;
    [SerializeField] AudioClip m_se;

    private int m_textIndex;

    private void Awake()
    {
        m_textIndex = 0;
        m_textAnimation.PlayText(m_texts[m_textIndex]);
    }

    private void Update()
    {
        // キーまたはゲームパッドのボタンが押されたら次のテキストへ
        if (Keyboard.current.anyKey.wasPressedThisFrame ||
            Gamepad.current?.buttonSouth.wasPressedThisFrame == true)
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        //効果音を再生
        SoundManager.Play2D(m_se);

        m_textIndex++;
        if (m_textIndex >= m_texts.Count)
        {
            //BGMの停止
            BGM.Instance.Stop();

            //シーン遷移
            Fade.FadeOut(1.0f, () =>
            {
                //シーンの破棄
                SceneController.UnLoad(SceneType.Tutorial);

                //シーンの読み込み
                SceneController.Load(SceneType.Player);
                SceneController.Load(SceneType.Select);

                //ステージ選択シーンのBGMを再生
                BGM.Instance.Play(SceneType.Select);

                //フェードイン
                Fade.FadeIn(1.0f);
            });
        }
        else
        {
            m_textAnimation.PlayText(m_texts[m_textIndex]);
        }

    }
}