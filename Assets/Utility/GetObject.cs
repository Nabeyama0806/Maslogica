using UnityEngine;

public class GetObject : MonoBehaviour
{
    static private GetObject m_instance;

    static public GetObject Instance
    {
        get { return m_instance; }
    }

    private GameObject m_player;

    public GameObject Player
    {
        get
        {
            if (m_player == null) m_player = GameObject.FindGameObjectWithTag("Player");
            return m_player;
        }
    }

    private void Awake()
    {
        m_instance = this;
    }
}
