using UnityEngine;

public class Heal : MonoBehaviour
{
    private float m_position;
    private float m_rotationY;

    void Start()
    {
        m_position = transform.position.y;
        m_rotationY = 30.0f;
    }

    void FixedUpdate()
    {
        //è„â∫Ç…à⁄ìÆ
        transform.position = new Vector3(
            transform.position.x,
            m_position + Mathf.PingPong(Time.time / 3, 0.3f),
            transform.position.z
            );

        //âÒì]
        transform.Rotate(
            0.0f,
            m_rotationY * Time.deltaTime,
            0.0f
        );
    }
}
