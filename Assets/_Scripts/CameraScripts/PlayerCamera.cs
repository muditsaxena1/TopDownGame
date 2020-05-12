using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float smoothingEffect;
    public GameObject minBound;
    public GameObject maxBound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position!=target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x,target.position.y,transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x,minBound.transform.position.x,maxBound.transform.position.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBound.transform.position.y, maxBound.transform.position.y);
            transform.position = Vector3.Lerp(transform.position,targetPosition,smoothingEffect);
        }
        
    }
}
