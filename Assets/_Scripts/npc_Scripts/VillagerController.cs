using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VillagerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D myRigidBody;
    public bool isWalking;
    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    private int walkDirection;

    private Animator anim;
    public Collider2D walkZone;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;
    private bool hasWalkZone;

    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        walkCounter = walkTime;
        waitCounter = waitTime;
        ChooseDirection();

        if (walkZone!=null)
        {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        moveNpc();
    }

    public void moveNpc()
    {
        if (isWalking)
        {
            walkCounter -= Time.deltaTime;

            switch (walkDirection)
            {
                case 0:
                    myRigidBody.velocity = new Vector2(0, moveSpeed);
                    if (hasWalkZone && transform.position.y > maxWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    anim.SetFloat("MoveY", 1);
                    break;
                case 1:
                    myRigidBody.velocity = new Vector2(moveSpeed, 0);
                    if (hasWalkZone && transform.position.x > maxWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    anim.SetFloat("MoveX", 1);
                    break;
                case 2:
                    myRigidBody.velocity = new Vector2(0, -moveSpeed);
                    if (hasWalkZone && transform.position.y < minWalkPoint.y)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    anim.SetFloat("MoveY", -1);
                    break;
                case 3:
                    myRigidBody.velocity = new Vector2(-moveSpeed, 0);
                    if (hasWalkZone && transform.position.x < minWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }
                    anim.SetFloat("MoveX", -1);
                    break;


            }

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
        }
        else
        {
            waitCounter -= Time.deltaTime;
            myRigidBody.velocity = Vector2.zero;
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 0);

            if (waitCounter < 0)
            {
                ChooseDirection();

            }
        }
    }
    public void ChooseDirection()
    {
        //choosing random number for directions 0: up,1:right
        //2:down,3:left
        walkDirection = Random.Range(0,4);
        isWalking = true;
        walkCounter = walkTime;
    }
}
