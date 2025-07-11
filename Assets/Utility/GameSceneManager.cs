using UnityEngine;
using UnityEngine.Events;
using static GameSceneManager;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] PlayerController m_player;
    [SerializeField] EnemyController m_enemy;

    public enum Phase
    {
        Start,
        PlayerTurn, 
        EnemyTurn,
        Check,
        Finish,

        Length,
    }

    private Phase m_phase;
    private Phase m_nextPhase;


    private void Awake()
    {
        //�}�E�X�J�[�\�����\���ɂ���
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        m_phase = Phase.Check;
        m_nextPhase = Phase.PlayerTurn;
    }

    private void FixedUpdate()
    {
        Debug.Log(m_phase);

        switch (m_phase)
        {
        case Phase.Start:
                Debug.Log("�Q�[���X�^�[�g");
                m_phase = Phase.PlayerTurn;
                break;

        case Phase.PlayerTurn:
                //�v���C���[�̑��삪�I������܂őҋ@
                if (!m_player.IsTurnEnd()) return;
                m_nextPhase = Phase.EnemyTurn;
                m_phase = Phase.Check;
                break;

        case Phase.EnemyTurn:
                //�G�l�~�[�̍s�����I������܂őҋ@
                if (m_enemy.IsPlay()) return;
                m_nextPhase = Phase.PlayerTurn;
                m_phase = Phase.Check;
                break;

        case Phase.Check:
                m_phase = m_nextPhase;
                if(m_phase == Phase.PlayerTurn) m_player.Play();
                break;

        case Phase.Finish:            
                //ChengeScene.Load(m_nextScene);
                break;
        }
    }

    public void ChengePhase(Phase phase)
    {
        m_phase = phase;
    }

    public Phase GetPhase() 
    {
        return m_phase;
    }

}
