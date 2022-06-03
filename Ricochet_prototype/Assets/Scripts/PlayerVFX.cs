using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVFX : MonoBehaviour
{

    public GameObject dotPrefab;
    public int dotAmount;

    float m_dotGap;

    GameObject[]  m_dotArray;

    // Start is called before the first frame update
    void Start()
    {
        m_dotGap= 1f/ dotAmount; //percentage of one dot relative to whole
        spawnDots();
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
            Vector3 _targetPos= Vector2.Lerp(startPos, endPos, i * m_dotGap);

            m_dotArray[i].transform.position= _targetPos;
        }
    }

    public void changeDotActiveState(bool state){
        for (int i = 0; i < dotAmount; i++)
        {
            m_dotArray[i].SetActive(state);   
        }
    }

}
