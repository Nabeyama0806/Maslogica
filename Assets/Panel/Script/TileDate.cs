using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDate : MonoBehaviour
{
    [SerializeField] GameObject m_tile;
    [SerializeField] GameObject m_effect;       

    private bool m_isActive;

    public bool IsActive
    {
        get { return m_isActive; }
        set { m_isActive = value; }
    }

    private void Start()
    {
        m_isActive = false;
        m_effect.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_isActive = !m_isActive;
            m_effect.SetActive(m_isActive);
        }
    }
}