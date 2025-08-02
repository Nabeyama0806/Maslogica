using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void OnClick()
    { 
        SceneController.Transition("Battle");
    }
}