using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputData inputData;
    public float moveSpeed= 5;

    Vector3 m_clickedPos;
    Vector3 m_releasePos;
    Vector3 m_dir; //direction

    Rigidbody2D m_rigid2D;

    Camera m_cam;

    PlayerVFX m_playerVfx;

    void Start() {
        GetComponent();
    }

    void Update(){
        HandleMovement();
    }

    void GetComponent(){
        m_rigid2D= GetComponent<Rigidbody2D>(); 
        m_cam= FindObjectOfType<Camera>();
        m_playerVfx= GetComponent<PlayerVFX>();
    }

    void HandleMovement(){

        if(inputData.isPressed){
            m_clickedPos= m_cam.ScreenToWorldPoint(Input.mousePosition);
            m_clickedPos= new Vector3(m_clickedPos.x, m_clickedPos.y, 0f);  

            resetPlayerPos();

            m_playerVfx.setDotStartPos(m_clickedPos);
            m_playerVfx.changeDotActiveState(true);
        }

        if (inputData.isHeld){
            m_playerVfx.setDotPos(m_clickedPos, m_cam.ScreenToWorldPoint(Input.mousePosition));
            m_playerVfx.pulsePlayer();
        }

        if (inputData.isReleased){
            m_releasePos= m_cam.ScreenToWorldPoint(Input.mousePosition);
            m_releasePos= new Vector3(m_releasePos.x, m_releasePos.y, 0f);

            m_playerVfx.changeDotActiveState(false);
            m_playerVfx.resetPlayerSize();

            CalculateDirection();
            MovePlayerInDirection();
        }
    }

    void CalculateDirection(){
        m_dir= (m_releasePos-m_clickedPos).normalized;
        Debug.Log(m_dir);
    }

    void MovePlayerInDirection(){
        m_rigid2D.velocity= m_dir * moveSpeed;
    }

    void resetPlayerPos(){
        transform.position= m_clickedPos;
        m_rigid2D.velocity= Vector3.zero;
    }

}
