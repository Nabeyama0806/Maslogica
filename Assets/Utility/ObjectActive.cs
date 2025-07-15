using UnityEngine;

public class ObjectActive : MonoBehaviour
{
    [SerializeField] GameObject obj;

    public void OnClick()
    {
        obj.SetActive(true);
    }
}