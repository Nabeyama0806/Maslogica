using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultSceneManager : MonoBehaviour
{
    private void Awake()
    {
        //ƒV[ƒ“‚Ì”jŠü
        SceneController.UnLoad(SceneType.Player);
        SceneController.Redo(SceneType.System);
    }
}
