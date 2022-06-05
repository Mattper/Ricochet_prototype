using UnityEngine;
using System;

public class Block : MonoBehaviour
{   
    public event Action OnBeinghit;

    private void OnCollisionEnter2D(Collision2D other) {
        if (OnBeinghit != null)
        {
            OnBeinghit();
        }

        gameObject.SetActive(false);
    }
}
