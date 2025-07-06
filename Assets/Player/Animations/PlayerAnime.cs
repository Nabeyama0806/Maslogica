using UnityEngine;

public class PlayerAnime : MonoBehaviour
{ 
    static private Animator m_animator;

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    static public void Run(bool isMove)
    {
        m_animator.SetBool("Run", isMove);
    }
}
