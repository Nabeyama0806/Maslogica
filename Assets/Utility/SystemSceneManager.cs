using UnityEngine;

public class SystemSceneManager : MonoBehaviour
{
    private void Awake()
    {
        //ƒQ[ƒ€ƒV[ƒ“‚ğ’Ç‰Á
        SceneController.Load("Game");
    }
}