using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileDate : MonoBehaviour
{
    [SerializeField] GameObject[] m_effects;
    [SerializeField] GameObject m_frame;
    [SerializeField] Material m_material;
    [SerializeField] AudioClip m_active;

    private Vector2Int m_tilePos;
    private bool m_isActive;

    public enum EffectType
    {
        Active,
        PlayerAttack,
        EnemyAttack,

        Length,
    }

    public bool IsActive
    {
        get { return m_isActive; }
        set { m_isActive = value; }
    }

    private void Start()
    {        
        //盤面座標
        m_tilePos = new Vector2Int((int)transform.localPosition.x, (int)transform.localPosition.z);

        //状態のリセット
        Passive();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //状態の反転
            m_isActive = TileGrid.Flip(m_tilePos);
            m_effects[(int)EffectType.Active].SetActive(m_isActive);

            //効果音
            SoundManager.Play2D(m_active, 0.4f);
        }
    }

    public void Passive()
    {
        m_isActive = false;
        m_effects[(int)EffectType.Active].SetActive(false);
    }

    public void PlayEffect(EffectType type)
    {
        //エフェクトの生成と削除
        GameObject effect = Instantiate(
            m_effects[(int)type],
            transform.position,
            Quaternion.identity
        );

        Destroy(effect, 1.0f);
    }

    public void EnemyAttack()
    {
        m_frame.GetComponent<MeshRenderer>().material = m_material;
        m_effects[(int)EffectType.EnemyAttack].SetActive(true);
    }
}