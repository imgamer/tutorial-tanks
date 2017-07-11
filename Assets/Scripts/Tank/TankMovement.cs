using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         // 用来指示这是哪一个玩家
    public float m_Speed = 12f;            // 坦克的每秒移动速度，m/s
    public float m_TurnSpeed = 180f;       // 坦克的转向速度，degree/s
    public AudioSource m_MovementAudio;    // 引用播放坦克引擎声音的音源
    public AudioClip m_EngineIdling;       // 坦克空闲时的声音
    public AudioClip m_EngineDriving;      // 坦克开动时的声音
    public float m_PitchRange = 0.2f;      // pitch（音高），vary(变化，变奏)。发动机噪声的音高变化量

    public string m_MovementAxisName;     // 控制前后移动输入轴的名字
    public string m_TurnAxisName;         // 控制转向输入轴的名字
    public Rigidbody m_Rigidbody;         // 用于移动坦克，需要物理碰撞效果
    public float m_MovementInputValue;    // 移动输入的当前值
    public float m_TurnInputValue;        // 转向输入的当前值
    public float m_OriginalPitch;         // 开始时场景的声音大小

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }

    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }

    private void Update()
    {
        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

        EngineAudio();
    }


    private void PlayAudioClip(AudioClip p_clip)
    {
        if (m_MovementAudio.clip != p_clip)
        {
            m_MovementAudio.clip = p_clip;
            m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
            m_MovementAudio.Play();
        }
    }

    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
        if(Mathf.Abs(m_MovementInputValue) < 0.1f || Mathf.Abs(m_TurnInputValue) < 0.1f)
            PlayAudioClip(m_EngineIdling);
        else
            PlayAudioClip(m_EngineDriving);
    }

    private void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
        Turn();
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
        Vector3 movement = transform.forward * m_MovementInputValue * m_Speed * Time.deltaTime; // 根据输入、速度和帧之间的时间，在坦克面向的方向上创建一个矢量(magnitude)
        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0.0f, turn, 0.0f);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
    }
}