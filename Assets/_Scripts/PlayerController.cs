using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    private Rigidbody2D myRigidbody;
    private Animator anim;
    private float oneBySquareRoot = 0.7072f;

    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        GetInput();
    }

    void GetInput()
    {
        float xInpt = StepFunction(Input.GetAxisRaw("Horizontal"));
        float yInpt = StepFunction(Input.GetAxisRaw("Vertical"));

        MovePlayer(new Vector2(xInpt, yInpt));
    }

    float StepFunction(float val)
    {
        if(val >= 0.5f)
        {
            return 1f;
        }
        if(val <= -0.5f)
        {
            return -1f;
        }
        return 0f;
    }



    void MovePlayer(Vector2 direction)
    {
        //Animation
        switch (direction.x)
        {
            case -1f:
                anim.SetBool("Left",true);
                anim.SetBool("Right", false);
                break;
            case 0f:
                anim.SetBool("Left", false);
                anim.SetBool("Right", false);
                break;
            case 1f:
                anim.SetBool("Left", false);
                anim.SetBool("Right", true);
                break;
            default:
                Debug.LogWarning("direction.x should have values among -1,0,1. Found to be" + direction.x);
                break;
        }
        switch (direction.y)
        {
            case -1f:
                anim.SetBool("Down", true);
                anim.SetBool("Up", false);
                break;
            case 0f:
                anim.SetBool("Down", false);
                anim.SetBool("Up", false);
                break;
            case 1f:
                anim.SetBool("Down", false);
                anim.SetBool("Up", true);
                break;
            default:
                Debug.LogWarning("direction.y should have values among -1,0,1. Found to be" + direction.y);
                break;
        }
        Vector2 currPos = new Vector2(transform.position.x, transform.position.y);
        if(Mathf.Abs(direction.x) == 1f && Mathf.Abs(direction.y) == 1f)
        {
            direction = direction * oneBySquareRoot;
        }
        direction = direction * speed * Time.deltaTime;
        myRigidbody.MovePosition(currPos + direction);
    }
}
