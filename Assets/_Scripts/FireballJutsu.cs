using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballJutsu : MonoBehaviour
{
    [SerializeField]
    float speed = 3f;
    public float atkDamage = 30f;

    private bool started;
    Vector3 direction;

    private void Awake()
    {
        direction = new Vector3(1f,0f,0f).normalized;
        started = false;
    }

    public void SetDirection(Vector3 dir)
    {
        //dir should be normalised
        direction = dir;
        started = true;
        StartCoroutine(DestroyAfterSeconds(5f));
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (started)
        {
            transform.position = transform.position + direction * speed * Time.deltaTime;
        }
    }
    

    IEnumerator DestroyAfterSeconds(float t)
    {
        yield return new WaitForSeconds(t);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        Debug.Log("Collided with " + target.name);
        if(target.tag == "Player" || target.tag == "Enemy")
        {
            target.GetComponent<HealthManager>().TakeDamage(atkDamage);
        }
        if(target.tag != "Ground Obstacles")
        {
            Destroy(this.gameObject);
        }
    }
}
