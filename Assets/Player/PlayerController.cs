using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed;         //移動速度
    [SerializeField] float m_jumpPower;         //ジャンプ力
    [SerializeField] float m_gravity;           //重力
    [SerializeField] float m_initFallSpeed;     //落下の初速

    private CharacterController m_characterController;
    private PlayerInput m_playerInput;
    private Vector3 m_moveVelocity;     //移動量
    private Vector3 m_inputValue;       //入力
    private bool m_canMove;             //移動可能か

    void Awake()
    {
        m_canMove = true;

        //コンポーネントの取得
        m_characterController = GetComponent<CharacterController>();
        m_playerInput = GetComponent<PlayerInput>();
    }

    private void OnEnable()
    {
        //入力時のイベントを設定
        m_playerInput.actions["Move"].performed += OnMove;
        m_playerInput.actions["Move"].canceled += OnMoveCancel;

        m_playerInput.actions["Attack"].performed += OnAttack;

        m_playerInput.actions["Jump"].performed += OnJump;
    }

    private void OnDisable()
    {
        //設定したイベントの除外
        m_playerInput.actions["Move"].performed -= OnMove;
        m_playerInput.actions["Move"].canceled -= OnMoveCancel;

        m_playerInput.actions["Attack"].performed -= OnAttack;

        m_playerInput.actions["Jump"].performed -= OnJump;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        //移動量の取得
        Vector2 input = context.ReadValue<Vector2>();
        m_inputValue = new Vector3(input.x, m_inputValue.y, input.y);

        //アニメーション
        PlayerAnime.Run(true);
    }

    private void OnMoveCancel(InputAction.CallbackContext context)
    {
        //移動量をなくす
        m_inputValue = Vector3.zero;

        //アニメーション
        PlayerAnime.Run(false);
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        //攻撃中はジャンプ不可
        if (!m_canMove) return;

        //接地していれば上方向に速度を与える
        if (!m_characterController.isGrounded) return;
        m_inputValue.y = m_jumpPower;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        //盤面のリセット
        TileGrid.PassiveAll();
    }

    void FixedUpdate()
    {
        //自由落下
        m_inputValue.y -= m_gravity * Time.deltaTime;

        //カメラの向きを考慮した移動量
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        m_moveVelocity = cameraForward * m_inputValue.z + Camera.main.transform.right * m_inputValue.x;
        m_moveVelocity = new Vector3(m_moveVelocity.x * m_moveSpeed, m_inputValue.y, m_moveVelocity.z * m_moveSpeed);

        //移動
        if (m_canMove)
        {
            m_characterController.Move(m_moveVelocity * Time.deltaTime);
        }

        //回転
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