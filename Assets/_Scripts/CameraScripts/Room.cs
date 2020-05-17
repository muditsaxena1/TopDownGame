using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject virtualCamera;
    // Start is called before the first frame update
    public virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player")&& !collider.isTrigger)
        {
            virtualCamera.SetActive(false);
        }
    }
    
}
