using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{
    
    
    public GameObject dotPrefab;
    public int dotAmount;

    [Space]
    [Header("Line Variables")]

    public AnimationCurve followCurve;
    public float followSpeed;

    [Space]
    [Header("Pulse Variables")]

    public AnimationCurve expandCurve;
    public float expandAmount;
    public float expandSpeed;

    Vector3 m_startSize;
    Vector3 m_targetSize;
    float m_scrollAmount;

    float m_dotGap;

    GameObject[]  m_dotArray;


    // Start is called before the first frame update
    void Start()
    {
        m_dotGap= 1f/ dotAmount; //percentage of one dot relative to whole
        initPulseEffect();
        spawnDots();
    }

    void initPulseEffect(){
        m_startSize= transform.localScale;
        m_targetSize= m_startSize * expandAmount;
    }

    void spawnDots(){
        m_dotArray= new GameObject[dotAmount];

        for (int i = 0; i < dotAmount; i++)
        {
            GameObject _dot =Instantiate(dotPrefab);
            _dot.SetActive(false);
            m_dotArray[i]= _dot;
        }
    }

     public void setDotPos(Vector3 startPos, Vector3 endPos){
        for (int i = 0; i < dotAmount; i++)
        {   
            Vector3 _dotPos= m_dotArray[i].transform.position;
            Vector3 _targetPos= Vector2.Lerp(startPos, endPos, (i + 1) * m_dotGap);

            //Smooth line
            float _smoothSpeed= (1f - followCurve.Evaluate(i * m_dotGap)) * followSpeed;

            m_dotArray[i].transform.position= Vector2.Lerp(_dotPos, _targetPos, _smoothSpeed *Time.deltaTime);

        }
    }

    public void changeDotActiveState(bool state){
        for (int i = 0; i < dotAmount; i++)
        {
            m_dotArray[i].SetActive(state);   
        }
    }

    public void setDotStartPos(Vector3 pos){
        for (int i = 0; i < dotAmount; i++)
        {
            m_dotArray[i].transform.position= pos;
        }
    }

    public void pulsePlayer(){
        m_scrollAmount += Time.deltaTime * expandSpeed;
        float _percent= expandCurve.Evaluate(m_scrollAmount);
        transform.localScale= Vector2.Lerp(m_startSize, m_targetSize, _percent);
    }

    public void resetPlayerSize(){
        transform.localScale= m_startSize;
        m_scrollAmount= 0f;
    }
}
