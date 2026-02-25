using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : MonoBehaviour
{
    private Slider m_healthSlider;
    private CharacterStatus m_status;

    private void Start()
    {
        m_healthSlider = GetComponent<Slider>();
        m_status = transform.root.GetComponent<CharacterStatus>();
    }

    private void FixedUpdate()
    {
        m_healthSlider.value = m_status.Health.Normalized;
    }
}
