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
    private bool isAttacking = false;
    public float attackRate = 1f;      //n times per second
    private float nextAttackTime = 0f;

    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isAttacking && Time.time > nextAttackTime)
            {
                isAttacking = true;
                nextAttackTime = Time.time + 1f / attackRate;
            }
        }
    }

    void FixedUpdate()
    {
        GetInput();
    }

    void GetInput()
    {
        float xInpt = StepFunction(Input.GetAxisRaw("Horizontal"));
        float yInpt = StepFunction(Input.GetAxisRaw("Vertical"));
        if (isAttacking)
        {
            Debug.Log("Attack");
            isAttacking = false;
            Attack();
        }
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

    void Attack()
    {
        anim.SetTrigger("Attack");
    }
    //LastPos Parameter is used to assign int for 8 directions
    //3 4 5
    //2 X 6
    //1 0 7
    //For example 5 for Up-Right direction
    void MovePlayer(Vector2 direction)
    {
        //Animation
        if(direction.x == 0 && direction.y == 0)
        {
            anim.SetBool("KeyDown", false);
        }
        else if (direction.x == 0 && direction.y == -1)
        {
            anim.SetInteger("LastPos", 0);
            anim.SetBool("KeyDown", true);
        }
        else if(direction.x == -1 && direction.y == -1)
        {
            anim.SetInteger("LastPos", 1);
            anim.SetBool("KeyDown", true);
        }
        else if (direction.x == -1 && direction.y == 0)
        {
            anim.SetInteger("LastPos", 2);
            anim.SetBool("KeyDown", true);
        }
        else if(direction.x == -1 && direction.y == 1)
        {
            anim.SetInteger("LastPos", 3);
            anim.SetBool("KeyDown", true);
        }
        else if (direction.x == 0 && direction.y == 1)
        {
            anim.SetInteger("LastPos", 4);
            anim.SetBool("KeyDown", true);
        }
        else if(direction.x == 1 && direction.y == 1)
        {
            anim.SetInteger("LastPos", 5);
            anim.SetBool("KeyDown", true);
        }
        else if (direction.x == 1 && direction.y == 0)
        {
            anim.SetInteger("LastPos", 6);
            anim.SetBool("KeyDown", true);

        }
        else if (direction.x == 1 && direction.y == -1)
        {
            anim.SetInteger("LastPos", 7);
            anim.SetBool("KeyDown", true);
        }
        else
        {
            Debug.LogWarning("direction.x/y should have values among -1,0,1. Found to be " + direction.x + "/" + direction.y);
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
