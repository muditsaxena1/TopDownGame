using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    private Rigidbody2D myRigidbody;
    private Animator animator;

    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }


    void FixedUpdate()
    {
        GetInput();
    }

    void GetInput()
    {
        float xInpt = StepFunction(Input.GetAxisRaw("Horizontal"));
        float yInpt = StepFunction(Input.GetAxisRaw("Vertical"));


        if(xInpt != 0 || yInpt != 0)
        {
            MovePlayer(new Vector2(xInpt,yInpt));
        }
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
        Vector2 currPos = new Vector2(transform.position.x, transform.position.y);
        switch (direction.y * 3 + direction.x)
        {
            case -4f:
                //DownLeft
                break;
            case -3f:
                //Down
                break;
            case -2f:
                //DownRight
                break;
            case -1f:
                //Left
                break;
            case 4f:
                //UpRight
                break;
            case 3f:
                //Up
                break;
            case 2f:
                //UpLeft
                break;
            case 1f:
                //Right
                break;
            default:
                Debug.LogWarning("This should have never happended");
                break;
        }
        direction = direction * speed * Time.deltaTime;
        myRigidbody.MovePosition(currPos + direction);
    }
}
