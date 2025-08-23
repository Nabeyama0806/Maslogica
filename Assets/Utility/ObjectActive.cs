using UnityEngine;

public class ObjectActive : MonoBehaviour
{
    [SerializeField] GameObject obj;
    [SerializeField] AudioClip m_se;

    public void OnClick()
    {
        if(m_se)SoundManager.Play2D(m_se);
        obj.SetActive(true);
    }
}