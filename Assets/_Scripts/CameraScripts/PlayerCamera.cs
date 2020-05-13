using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    public float smoothingEffect;
    public GameObject minBound;
    public GameObject maxBound;
    public Vector2 minPosition;
    public Vector2 maxPosition;

    // Start is called before the first frame update
    void Start()
    {
        minPosition = minBound.transform.position;
        maxPosition = maxBound.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position!=target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x,target.position.y,transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x,minPosition.x,maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position,targetPosition,smoothingEffect);
        }
        
    }
}
