using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed;         //�ړ����x
    [SerializeField] float m_jumpPower;         //�W�����v��
    [SerializeField] float m_gravity;           //�d��
    [SerializeField] float m_initFallSpeed;     //�����̏���

    private CharacterController m_characterController;
    private PlayerInput m_playerInput;
    private Vector3 m_moveVelocity;     //�ړ���
    private Vector3 m_inputValue;       //����
    private bool m_canMove;             //�ړ��\��

    void Awake()
    {
        m_canMove = true;

        //�R���|�[�l���g�̎擾
        m_characterController = GetComponent<CharacterController>();
        m_playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        //���͎��̃C�x���g��ݒ�
        m_playerInput.actions["Move"].performed += OnMove;
        m_playerInput.actions["Move"].canceled += OnMoveCancel;

        m_playerInput.actions["Attack"].performed += OnAttack;

        m_playerInput.actions["Jump"].performed += OnJump;
    }

    private void OnDisable()
    {
        //�ݒ肵���C�x���g�̏��O
        m_playerInput.actions["Move"].performed -= OnMove;
        m_playerInput.actions["Move"].canceled -= OnMoveCancel;

        m_playerInput.actions["Attack"].performed -= OnAttack;

        m_playerInput.actions["Jump"].performed -= OnJump;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
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
        //�U�����̓W�����v�s��
        if (!m_canMove) return;

        //�ڒn���Ă���Ώ�����ɑ��x��^����
        if (!m_characterController.isGrounded) return;
        m_inputValue.y = m_jumpPower;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        //�Ֆʂ̃��Z�b�g
        TileGrid.PassiveAll();
    }

    void FixedUpdate()
    {
        //���R����
        m_inputValue.y -= m_gravity * Time.deltaTime;

        //�J�����̌������l�������ړ���
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        m_moveVelocity = cameraForward * m_inputValue.z + Camera.main.transform.right * m_inputValue.x;
        m_moveVelocity = new Vector3(m_moveVelocity.x * m_moveSpeed, m_inputValue.y, m_moveVelocity.z * m_moveSpeed);

        //�ړ�
        if (m_canMove)
        {
            m_characterController.Move(m_moveVelocity * Time.deltaTime);
        }

        //��]
        Vector3 move = new Vector3(m_inputValue.x, 0, m_inputValue.z);
        if (move != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                Quaternion.LookRotation(move.normalized),
                0.2f
                );
        }
    }
}