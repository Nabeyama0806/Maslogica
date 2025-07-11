using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealtSlider : MonoBehaviour
{
    [SerializeField] Health m_health;
    private float m_maxHealth;
    private Slider m_helathSlider;

    private void Start()
    {
        m_helathSlider = GetComponent<Slider>();

        //�̗͂̎擾
        m_helathSlider.maxValue = m_health.Value;
        m_helathSlider.value = m_maxHealth;

    }

    private void Update()
    {
        m_helathSlider.value = m_health.Value;
    }
}
