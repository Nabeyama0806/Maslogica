using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed;         //移動速度
    [SerializeField] float m_jumpPower;         //ジャンプ力
    [SerializeField] float MoveingTime = 8.0f;     //移動可能時間

    private CharacterController m_characterController;
    private PlayerInput m_playerInput;
    private Vector3 m_inputValue;      //入力
    private bool m_isMove;             //移動可能か
    private bool m_isPlay;             //行動中か
    private float m_moveingTime;       //残りの移動可能時間

    void Awake()
    {
        //コンポーネントの取得
        m_characterController = GetComponent<CharacterController>();
        m_playerInput = GetComponent<PlayerInput>();

        m_moveingTime = MoveingTime;
        m_isMove = false;
        m_isPlay = false;
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
        if (!m_isPlay) return;

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
        if (m_moveingTime <= 0) return;

        //接地していれば上方向に速度を与える
        if (!m_characterController.isGrounded) return;
        m_inputValue.y = m_jumpPower;
    }

    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("プレイヤーの攻撃!!!");
    }

    private void Update()
    {
        //自由落下
        m_inputValue.y += Physics.gravity.y * Time.deltaTime;
    }

    public bool IsPlay()    
    {
        m_isPlay = true;

        //動いてから計測
        if (m_isMove) m_moveingTime -= Time.deltaTime;

        //カメラの向きを考慮した移動量
        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveVelocity = cameraForward * m_inputValue.z + Camera.main.transform.right * m_inputValue.x;
        moveVelocity = new Vector3(moveVelocity.x * m_moveSpeed, m_inputValue.y, moveVelocity.z * m_moveSpeed);

        //移動
        m_characterController.Move(moveVelocity * Time.deltaTime);

        //回転
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

        //時間が無くなればターン終了
        if (m_moveingTime <= 0)
        {
            m_isPlay = false;

            //次のターンの準備
            m_moveingTime = MoveingTime;
            m_isMove = false;
        }

        return m_isPlay;
    }
}