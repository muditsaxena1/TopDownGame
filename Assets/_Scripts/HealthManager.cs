using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currHealth;
    public Slider slider;
    public Gradient gradient;
    public Image fill;

    // Start is called before the first frame update
    void Start()
    {
        currHealth = maxHealth;
        slider.maxValue = maxHealth;
        SetHealth(currHealth);
    }

    public void SetHealth(float health)
    {
        slider.value = health;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    public void TakeDamage(float damage)
    {
        currHealth -= damage;
        SetHealth(currHealth);
        if (currHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(this.name + " is dead");
        if (tag == "Player")
        {
            SceneManager.LoadScene(0);
        }
        else if(tag == "Enemy")
        {
            GetComponent<Animator>().SetBool("isDead", true);
            GetComponent<EnemyWizardAI>().beingDestroyed = true;
            StartCoroutine(DistroyEnemy());
        }
    }

    IEnumerator DistroyEnemy()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
}
