using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWizardAI : MonoBehaviour
{
    Vector3 startingPosition;
    Transform playerTransform;
    Rigidbody2D myRigidbody;
    Vector3 target;
    Animator anim;
    public bool beingDestroyed = false;

    [SerializeField]
    float roamingSpeed = 10f, followingSpeed = 12f;

    [SerializeField]
    float attackRate = 0.2f, nextAttackTime = 0f;

    [SerializeField]
    float attackDistance = 30f, followDistance = 50f, unfollowDistance = 70f;

    [SerializeField]
    float minMove = 50f, maxMove = 70f;

    [SerializeField]
    Vector2 fireStartingPointShift;

    [SerializeField]
    GameObject fireball;

    enum States
    {
        roaming,
        following,
        attacking
    }
    States currState;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        myRigidbody = this.GetComponent<Rigidbody2D>();
        startingPosition = this.transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        currState = States.roaming;
        target = startingPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (beingDestroyed)
        {
            return;
        }
        float separation = Vector3.Distance(playerTransform.position, transform.position);
        switch (currState)
        {
            case States.roaming:
                if (Vector3.Distance(transform.position, target) < 1f)
                {
                    target = startingPosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0f).normalized * Random.Range(minMove, maxMove);
                }
                MoveEnemy();
                if(separation < followDistance)
                {
                    currState = States.following;
                }
                break;
            case States.following:
                target = playerTransform.position;
                MoveEnemy();
                if (separation < attackDistance)
                {
                    currState = States.attacking;
                }
                else if (separation > unfollowDistance)
                {
                    currState = States.roaming;
                }
                break;
            case States.attacking:
                Vector3 direction = (playerTransform.position - transform.position);
                if (direction.x < 0)
                {
                    transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                }

                if (Time.time > nextAttackTime)
                {
                    nextAttackTime = Time.time + 1f / attackRate;
                    anim.SetTrigger("Attack");
                    StartCoroutine(StartFireJutsu(playerTransform));
                }
                
                if (separation > attackDistance)
                {
                    currState = States.following;
                }
                break;
        }
        
        if (separation < attackDistance)
        {
            currState = States.attacking;
        }
        else if(separation < unfollowDistance)
        {
            currState = States.following;
        }
        else
        {
            currState = States.roaming;
        }
    }

    IEnumerator StartFireJutsu(Transform target)
    {
        yield return new WaitForSeconds(1f);
        if (!beingDestroyed)
        {
            Vector3 startingPoint = transform.position + new Vector3((transform.localScale.x < 0 ? 1 : -1) * fireStartingPointShift.x, fireStartingPointShift.y, 0f);
            GameObject fire = Instantiate(fireball, startingPoint, Quaternion.identity) as GameObject;
            Vector3 dir = (target.position - startingPoint).normalized;
            fire.GetComponent<FireballJutsu>().SetDirection(dir);
        }
    }

    void MoveEnemy()
    {
        float speed = roamingSpeed;
        if(currState == States.following)
        {
            speed = followingSpeed;
        }
        Vector3 direction = (target - transform.position).normalized;
        if(direction.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x),transform.localScale.y,transform.localScale.z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        transform.position = transform.position + direction * speed * Time.deltaTime;
    }
}
