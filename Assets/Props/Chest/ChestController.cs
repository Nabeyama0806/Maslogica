using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class ChestController : MonoBehaviour
{
    [SerializeField] GameObject m_chestOpenEffect; 
    [SerializeField] AudioClip m_openSe;
    [SerializeField] AudioClip m_itemGetSe;

    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void Open()
    {
        //エフェクトの表示
        m_chestOpenEffect.SetActive(true);
    }
    public void OpenEnd()
    {
        //効果音の再生
        SoundManager.Play2D(m_itemGetSe);

        //シーン遷移
        SceneController.Transition("Select");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //効果音
            SoundManager.Play2D(m_openSe);

            //アニメーションの再生
            m_animator.SetTrigger("Open");
        }
    }
}
