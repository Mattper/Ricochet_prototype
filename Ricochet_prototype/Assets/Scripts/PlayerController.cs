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

    void Start() {
        GetComponent();
    }

    void Update(){
        HandleMovement();
    }

    void GetComponent(){
        m_rigid2D= GetComponent<Rigidbody2D>(); 
        m_cam= FindObjectOfType<Camera>();
    }

    void HandleMovement(){

        if(inputData.isPressed){
            m_clickedPos= m_cam.ScreenToWorldPoint(Input.mousePosition);
            m_clickedPos= new Vector3(m_clickedPos.x, m_clickedPos.y, 0f);  
        }

        if (inputData.isReleased){
            m_releasePos= m_cam.ScreenToWorldPoint(Input.mousePosition);
            m_releasePos= new Vector3(m_releasePos.x, m_releasePos.y, 0f);

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

}
