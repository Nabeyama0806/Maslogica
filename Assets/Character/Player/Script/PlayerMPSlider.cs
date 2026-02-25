using UnityEngine;
using UnityEngine.UI;

public class PlayerMPSlider : MonoBehaviour
{
    static private Slider m_mpSlider;

    private void Start()
    {
        m_mpSlider = GetComponent<Slider>();
    }

    static public void SetMP(float mp)
    {
        m_mpSlider.value = mp;
    }
}