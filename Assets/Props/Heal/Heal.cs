using UnityEngine;

public class Heal : MonoBehaviour
{
    [SerializeField] GameObject m_effect;
    [SerializeField] GameObject m_circle;
    [SerializeField] GameObject m_text;
    [SerializeField] AudioClip m_healSound;

    private float m_position;
    private float m_rotationY;

    void Start()
    {
        m_position = transform.position.y;
        m_rotationY = 30.0f;
    }

    void FixedUpdate()
    {
        //上下に移動
        transform.position = new Vector3(
            transform.position.x,
            m_position + Mathf.PingPong(Time.time / 3, 0.3f),
            transform.position.z
            );

        //回転
        transform.Rotate(
            0.0f,
            m_rotationY * Time.deltaTime,
            0.0f
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //プレイヤーの体力を回復
            CharacterStatus status = other.GetComponent<CharacterStatus>();
            status.Heal(status.Base.health);

            //効果音の再生
            SoundManager.Play2D(m_healSound);

            //テキストの表示
            m_text.SetActive(true);
            Destroy(m_text, 2.0f);

            //回復エフェクトの再生
            m_effect.SetActive(true);
            Destroy(m_effect, 0.8f);

            //回復エリアの非表示
            m_circle.SetActive(false);

            //自身の非表示
            gameObject.SetActive(false);
        }
    }
}
