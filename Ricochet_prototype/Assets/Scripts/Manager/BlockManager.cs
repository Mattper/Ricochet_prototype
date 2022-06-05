using UnityEngine;
using System;

public class BlockManager : MonoBehaviour
{
    public event Action OnMouseClick;
    
    public Block[] blockArray;
    [SerializeField] int m_blockCount;

    private void Start() {
        blockArray= FindObjectsOfType<Block>();
        m_blockCount= blockArray.Length;
        subscribeToEvent();
    }

    void subscribeToEvent(){
        foreach (Block block in blockArray)
        {
            block.OnBeinghit += decreaseBlockCount;
        }

        FindObjectOfType<PlayerController>().OnMouseClick += resetAllBlocks;

    }


    void decreaseBlockCount(){
        m_blockCount--;
    }

    void resetAllBlocks(){
        foreach (Block block in blockArray)
        {
            if (block.gameObject.activeSelf == false)
            {
                block.gameObject.SetActive(true);
            }
        }

        m_blockCount= blockArray.Length;
    }

}
