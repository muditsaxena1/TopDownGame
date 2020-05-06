using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    private Rigidbody2D myRigidbody;
    private Animation anim;
    private AnimationClip[] clips;

    void Start()
    {
        myRigidbody = this.GetComponent<Rigidbody2D>();
        anim = this.GetComponent<Animation>();
        clips = AnimationUtility.GetAnimationClips(this.gameObject);
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
        int animNumber = (int)(direction.y * 3 + direction.x);
        direction = direction * speed * Time.deltaTime;
        myRigidbody.MovePosition(currPos + direction);
        if (anim.isPlaying)
        {
            return;
        }
        anim.Play("CharacterDown");
    }
}
