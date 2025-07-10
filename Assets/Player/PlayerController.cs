using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float m_moveSpeed;         //移動速度
    [SerializeField] float m_jumpPower;         //ジャンプ力
    [SerializeField] float MoveingTime = 8.0f;  //移動可能時間
    [SerializeField] GameObject m_snapEffect;   //移動後のエフェクト
    [SerializeField] AudioClip m_turnEnd;

    private CharacterController m_characterController;
    private PlayerInput m_playerInput;
    private Vector3 m_inputValue;      //入力
    private bool m_isMove;             //移動したか
    private bool m_isPlay;             //行動中か
    private bool m_isTurnEnd;          //ターン終了か 
    private float m_moveingTime;       //残りの移動可能時間

    public bool IsTurnEndFlag
    { 
        set {   m_isTurnEnd = value; }
    }

    void Awake()
    {
        //コンポーネントの取得
        m_characterController = GetComponent<CharacterController>();
        m_playerInput = GetComponent<PlayerInput>();

        m_moveingTime = MoveingTime;
        m_isMove = false;
        m_isPlay = false;
        m_isTurnEnd = true;
    }

    private void OnEnable()
    {
        //入力時のイベントを設定
        m_playerInput.actions["Move"].performed += OnMove;
        m_playerInput.actions["Move"].canceled += OnMoveCancel;

        m_playerInput.actions["Jump"].performed += OnJump;
    }

    private void OnDisable()
    {
        //設定したイベントの除外
        m_playerInput.actions["Move"].performed -= OnMove;
        m_playerInput.actions["Move"].canceled -= OnMoveCancel;

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

        //移動していれば回転させる
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

        //移動可能時間が無くなれば終了
        if (m_moveingTime <= 0)
        {
            //次のターンの準備
            m_isPlay = false;
            m_isMove = false;
            m_moveingTime = MoveingTime;
        }

        return m_isPlay;
    }

    public bool IsTurnEnd()
    {
        //座標の調整
        transform.position = TileGrid.ToGridPos(transform.position);

        //エフェクトの生成と削除
        GameObject effect = Instantiate(m_snapEffect, transform);
        Destroy(effect, 1.0f);

        //攻撃アニメーション
        PlayerAnime.Attack();

        //効果音
        SoundManager.Play2D(m_turnEnd);

        return m_isTurnEnd;
    }
}