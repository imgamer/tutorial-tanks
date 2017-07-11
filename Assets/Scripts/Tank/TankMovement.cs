using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int m_PlayerNumber = 1;         // 用来指示这是哪一个玩家
    public float m_Speed = 12f;            // 坦克的每秒移动速度，m/s
    public float m_TurnSpeed = 180f;       // 坦克的转向速度，degree/s
    public AudioSource m_MovementAudio;    // 引用播放坦克引擎声音的音源
    public AudioClip m_EngineIdling;       // 坦克空闲时的声音
    public AudioClip m_EngineDriving;      // 坦克开动时的声音
    public float m_PitchRange = 0.2f;      // pitch（音高），发动机噪声的音高变化量

    /*
    private string m_MovementAxisName;     
    private string m_TurnAxisName;         
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;         


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
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
    */

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
    }


    private void FixedUpdate()
    {
        // Move and turn the tank.
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
    }
}