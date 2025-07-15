using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] string m_nextSceneName;

    public void OnClick()
    {
        SceneController.UnLoad("CardSelect");
    }
}
