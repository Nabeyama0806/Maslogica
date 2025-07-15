using UnityEngine;

public class Button : MonoBehaviour
{
    public void OnClick()
    {
        SceneController.UnLoad("CardSelect");
    }
}
