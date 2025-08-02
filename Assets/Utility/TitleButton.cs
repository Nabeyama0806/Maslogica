using UnityEngine;

public class TitleButton : MonoBehaviour
{
    [SerializeField] AudioClip m_se;

    public void OnClick()
    { 
        SoundManager.Play2D(m_se);
        SceneController.Transition("Battle");
    }
}