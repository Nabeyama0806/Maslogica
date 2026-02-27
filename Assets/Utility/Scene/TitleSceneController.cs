using UnityEngine;
using UnityEngine.InputSystem;

public class TitleSceneController : MonoBehaviour
{
    [SerializeField] TitleCameraMove m_titleCamera;
    [SerializeField] AudioClip m_se;

    private void Awake()
    {
        // タイトルシーンのBGMを再生
        BGM.Instance.Play(SceneType.Title);
    }

    void Update()
    {
        // キーまたはゲームパッドのボタンが押されたらゲーム開始
        if (Keyboard.current.anyKey.wasPressedThisFrame ||
            Gamepad.current?.buttonSouth.wasPressedThisFrame == true)
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        //カメラを徐々に加速させるフラグを立てる
        m_titleCamera.IsClick = true;

        //BGMの停止
        BGM.Instance.Stop();

        //効果音を再生
        SoundManager.Play2D(m_se);

        //シーン遷移
        SceneController.Transition(SceneType.Title, SceneType.Tutorial);
    }
}