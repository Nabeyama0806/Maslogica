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

        m_phase = Phase.Start;
    }

    private void FixedUpdate()
    {
        Debug.Log(m_phase);

        switch (m_phase)
        {
        case Phase.Start:
                Debug.Log("�Q�[���X�^�[�g");
                m_phase = Phase.Check;
                m_nextPhase = Phase.PlayerTurn;
                break;

        case Phase.PlayerTurn:
                //�v���C���[�̑��삪�I������܂őҋ@
                if (!m_player.IsTurnEnd()) return;
                m_nextPhase = Phase.EnemyTurn;
                m_phase = Phase.Check;
                break;

        case Phase.EnemyTurn:
                //�G�l�~�[�̍s�����I������܂őҋ@
                if (m_enemy.Play()) return;
                m_nextPhase = Phase.PlayerTurn;
                m_phase = Phase.Check;
                break;

        case Phase.Check:
                //���̃^�[�����擾
                m_phase = m_nextPhase;

                //�Ֆʂ̊m�F
                if (m_phase == Phase.PlayerTurn)
                {
                    m_player.Play();
                }
                else
                {
                    if (TileGrid.Check()) m_enemy.GetComponent<Health>().Damage(10);
                    m_enemy.Play();
                }

                //�Ֆʂ̃��Z�b�g
                TileGrid.PassiveAll();
                break;

        case Phase.Finish:            
                //ChengeScene.Load(m_nextScene);
                break;
        }
    }
}
