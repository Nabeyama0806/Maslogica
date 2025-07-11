using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMPSlider : MonoBehaviour
{
    static private Slider m_mpSlider;

    private void Start()
    {
        m_mpSlider = GetComponent<Slider>();
    }

    static public void SetMaxMP(float maxMP)
    {
        m_mpSlider.maxValue = maxMP;
        m_mpSlider.value = maxMP;
    }
    static public void SetMP(float mp)
    {
        m_mpSlider.value = mp;
    }
}