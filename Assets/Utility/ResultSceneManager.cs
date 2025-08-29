using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    private void Awake()
    {
        //ƒV[ƒ“‚Ì”jŠü
        SceneController.UnLoad(SceneController.Type.Player);
        SceneController.Redo(SceneController.Type.System);
    }
}
