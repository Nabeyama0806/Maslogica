using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDate : MonoBehaviour
{
    [SerializeField] GameObject m_tile;
    [SerializeField] Material m_defaultColor;   //’Êí‚ÌF
    [SerializeField] Material m_hitColor;       //ÚG‚ÌF

    private MeshRenderer m_meshRenderer;
    private bool m_isActive;

    public bool IsActive
    {
        get { return m_isActive; }
        set { m_isActive = value; }
    }

    private void Start()
    {
        m_isActive = false;
        m_meshRenderer = m_tile.GetComponent<MeshRenderer>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_isActive = !m_isActive;
            m_meshRenderer.material = m_isActive ? m_hitColor : m_defaultColor;
        }
    }
}
