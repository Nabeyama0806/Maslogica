using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileDate : MonoBehaviour
{
    [SerializeField] GameObject m_effect;

    private Vector2Int m_tilePos;
    private bool m_isActive;

    enum Type
    {   
        Normal, //�ʏ�}�X
        Damage, //�G�l�~�[�̍U���\���}�X

        Length,
    }

    public bool IsActive
    {
        get { return m_isActive; }
        set { m_isActive = value; }
    }

    private void Start()
    {
        m_isActive = false;
        m_effect.SetActive(false);

        //�Ֆʍ��W
        m_tilePos = new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //��Ԃ̔��]
            m_isActive = TileGrid.Flip(m_tilePos);
            m_effect.SetActive(m_isActive);
        }
    }

    public void Passive()
    {
        m_isActive = false;
        m_effect.SetActive(m_isActive);
    }
}