using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTakingDamage : MonoBehaviour
{
    public Animator anim;
    public int MaxHealth =100;
    int currentHealth;
    bool isdead = false;
    [SerializeField] private AudioSource takingHit;
    private void Start()
    {
        currentHealth = MaxHealth;
        
        anim = GetComponent<Animator>();
        
        
    }

    public void TakingDamage(int damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("Hurt");
        
  
        if (currentHealth < 0)
        {
            Die();
        }

       
    }
    void Die()
    {
        anim.SetBool("IsDead", true);
        takingHit.Play();
        
        isdead = true;
        this.enabled = false;
        gameObject.GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AfterLife());
        GetComponent<EnemyPatrol>().enabled = false;

    }

    IEnumerator AfterLife()
    {
        if (isdead == true)
        {
            yield return new WaitForSecondsRealtime(3);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject.Destroy(gameObject);
        }

    }


}
