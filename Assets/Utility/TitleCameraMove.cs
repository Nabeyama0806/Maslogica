using UnityEngine;

public class TitleCameraMove : MonoBehaviour
{
    [SerializeField] float m_speed;

    private const float AddSpeed = 0.003f; //カメラの移動速度を徐々に上げる値

    private bool m_isClick;

    public bool IsClick
    {
        set { m_isClick = value; }
    }

    private void Start()
    {
        m_isClick = false;
    }

    private void FixedUpdate()
    {
        //前方に移動
        transform.position += new Vector3(0.0f, 0.0f, m_speed);
        if(m_isClick) m_speed += AddSpeed; 
    }
}
