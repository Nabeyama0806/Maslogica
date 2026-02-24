using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    [SerializeField] CharacterStatus m_status;
    private Slider m_healthSlider;

    private void Start()
    {
        m_healthSlider = GetComponent<Slider>();

        //�̗͂̎擾
        m_healthSlider.maxValue = m_status.Base.maxHealth;
        m_healthSlider.value = m_healthSlider.maxValue;
    }

    private void FixedUpdate()
    {
        m_healthSlider.value = m_status.CurrentHealth;
    }
}
