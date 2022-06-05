using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public InputData inputData;
    public LayerMask layerToCollideWith;
    public event Action OnMouseClick;
    public float moveSpeed= 5;

    Vector3 m_clickedPos;
    Vector3 m_releasePos;
    Vector3 m_dir; //direction

    Rigidbody2D m_rigid2D;

    Camera m_cam;

    PlayerVFX m_playerVfx;

    bool m_hitblock;

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
            
            m_hitblock= checkIfHitBlock();
            if (m_hitblock)
                return;

            m_clickedPos= m_cam.ScreenToWorldPoint(Input.mousePosition);
            m_clickedPos= new Vector3(m_clickedPos.x, m_clickedPos.y, 0f);  

            resetPlayerPos();

            m_playerVfx.setDotStartPos(m_clickedPos);
            m_playerVfx.changeDotActiveState(true);
            m_playerVfx.changeTrailState(false, 0);

            if (OnMouseClick != null)
            {
                OnMouseClick();
            }

            //OnMouseClick?.Invoke();
        }

        if (inputData.isHeld){

            if (m_hitblock)
                return;

            m_playerVfx.setDotPos(m_clickedPos, m_cam.ScreenToWorldPoint(Input.mousePosition));
            m_playerVfx.pulsePlayer();
        }

        if (inputData.isReleased){

            if (m_hitblock)
                return;

            m_releasePos= m_cam.ScreenToWorldPoint(Input.mousePosition);
            m_releasePos= new Vector3(m_releasePos.x, m_releasePos.y, 0f);

            m_playerVfx.changeDotActiveState(false);
            m_playerVfx.resetPlayerSize();
            m_playerVfx.changeTrailState(true, 0.75f);

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

    //Player Collision
    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("block"))
        {
            Vector2 _wallNormal= other.contacts[0].normal;
            m_dir= Vector2.Reflect(m_rigid2D.velocity, _wallNormal).normalized;

            m_rigid2D.velocity= m_dir * moveSpeed;
        }    
    }

    bool checkIfHitBlock(){
        Ray _ray= m_cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D _hitBlock= Physics2D.Raycast(_ray.origin, _ray.direction, 100f, layerToCollideWith );

        return _hitBlock;
    }

}
