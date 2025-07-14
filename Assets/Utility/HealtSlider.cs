using UnityEngine;
using UnityEngine.UI;

public class HealtSlider : MonoBehaviour
{
    [SerializeField] Health m_health;
    private float m_maxHealth;
    private Slider m_healthSlider;

    private void Start()
    {
        m_healthSlider = GetComponent<Slider>();

        //�̗͂̎擾
        m_healthSlider.maxValue = m_health.Value;
        m_healthSlider.value = m_maxHealth;

    }

    private void FixedUpdate()
    {
        m_healthSlider.value = m_health.Value;
    }
}
