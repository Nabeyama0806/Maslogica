using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed;         //�ړ����x
    [SerializeField] float m_jumpPower;         //�W�����v��
    [SerializeField] float MoveingTime = 8.0f;  //�ړ��\����
    [SerializeField] GameObject m_snapEffect;   //�ړ���̃G�t�F�N�g
    [SerializeField] AudioClip m_turnEnd;

    private CharacterController m_characterController;
    private PlayerInput m_playerInput;
    private Vector3 m_inputValue;      //����
    private bool m_isMove;             //�ړ�������
    private bool m_isPlay;             //�s������
    private bool m_isTurnEnd;          //�^�[���I���� 
    private float m_moveingTime;       //�c��̈ړ��\����

    public bool IsTurnEndFlag
    { 
        set {   m_isTurnEnd = value; }
    }

    void Awake()
    {
        //�R���|�[�l���g�̎擾
        m_characterController = GetComponent<CharacterController>();
        m_playerInput = GetComponent<PlayerInput>();

        m_moveingTime = MoveingTime;
        m_isMove = false;
        m_isPlay = false;
        m_isTurnEnd = true;
    }

    private void OnEnable()
    {
        //���͎��̃C�x���g��ݒ�
        m_playerInput.actions["Move"].performed += OnMove;
        m_playerInput.actions["Move"].canceled += OnMoveCancel;

        m_playerInput.actions["Jump"].performed += OnJump;
    }

    private void OnDisable()
    {
        //�ݒ肵���C�x���g�̏��O
        m_playerInput.actions["Move"].performed -= OnMove;
        m_playerInput.actions["Move"].canceled -= OnMoveCancel;

        m_playerInput.actions["Jump"].performed -= OnJump;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        if (!m_isPlay) return;

        //�ړ��ʂ̎擾
        Vector2 input = context.ReadValue<Vector2>();
        m_inputValue = new Vector3(input.x, m_inputValue.y, input.y);

        //�A�j���[�V����
        PlayerAnime.Run(true);
    }

    private void OnMoveCancel(InputAction.CallbackContext context)
    {
        //�ړ��ʂ��Ȃ���
        m_inputValue = Vector3.zero;

        //�A�j���[�V����
        PlayerAnime.Run(false);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (m_moveingTime <= 0) return;

        //�ڒn���Ă���Ώ�����ɑ��x��^����
        if (!m_characterController.isGrounded) return;
        m_inputValue.y = m_jumpPower;
    }

    private void Update()
    {
        //���R����
        m_inputValue.y += Physics.gravity.y * Time.deltaTime;
    }

    public bool IsPlay()    
    {
        m_isPlay = true;

        //�����Ă���v��
        if (m_isMove) m_moveingTime -= Time.deltaTime;

        //�J�����̌������l�������ړ���
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveVelocity = cameraForward * m_inputValue.z + Camera.main.transform.right * m_inputValue.x;
        moveVelocity = new Vector3(moveVelocity.x * m_moveSpeed, m_inputValue.y, moveVelocity.z * m_moveSpeed);

        //�ړ�
        m_characterController.Move(moveVelocity * Time.deltaTime);

        //�ړ����Ă���Ή�]������
        Vector3 move = new Vector3(m_inputValue.x, 0, m_inputValue.z);
        if (move != Vector3.zero)
        {
            m_isMove = true;

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(move.normalized),
                0.2f
            );
        }

        //�ړ��\���Ԃ������Ȃ�ΏI��
        if (m_moveingTime <= 0)
        {
            //���̃^�[���̏���
            m_isPlay = false;
            m_isMove = false;
            m_moveingTime = MoveingTime;
        }

        return m_isPlay;
    }

    public bool IsTurnEnd()
    {
        //���W�̒���
        transform.position = TileGrid.ToGridPos(transform.position);

        //�G�t�F�N�g�̐����ƍ폜
        GameObject effect = Instantiate(m_snapEffect, transform);
        Destroy(effect, 1.0f);

        //�U���A�j���[�V����
        PlayerAnime.Attack();

        //���ʉ�
        SoundManager.Play2D(m_turnEnd);

        return m_isTurnEnd;
    }
}