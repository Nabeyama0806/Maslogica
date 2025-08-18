using UnityEngine;

public class TitleButton : MonoBehaviour
{
    [SerializeField] GameObject m_titleBgm;
    [SerializeField] AudioClip m_se;

    public void OnClick()
    {
        //ƒ^ƒCƒgƒ‹BGM‚ð’âŽ~
        m_titleBgm.SetActive(false);

        SoundManager.Play2D(m_se);
        SceneController.Transition("Select");
    }
}