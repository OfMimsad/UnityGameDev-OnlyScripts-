using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerAttack : MonoBehaviour
{
    Animator anim;

    public  Transform attackPoint;
    public LayerMask enemyLayers;
    public  float AttackRange = 0.5f;
    public  int AttackDamage = 30;
    bool InAir;
    public float AttackingRate = 5;
    public float nextAttacking = 0f;
    public MainPlayerMovement die;

    [SerializeField] private AudioSource sword;

    void Start()
    {
        anim = GetComponent<Animator>();
       die =  GetComponent<MainPlayerMovement>();
    }

   
    void Update() {

        if (Time.time >= nextAttacking && GetComponent<MainPlayerMovement>().currentHealth >= 0)
        {
            

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    if (InAir == false)
                    {
                        Attack();
                        anim.SetTrigger("Attack1");
                        nextAttacking = Time.time + 1f / AttackingRate;
                        sword.Play();

                    }
                    else if (InAir == true)
                    {
                        Attack();
                        anim.SetTrigger("InAirAttack");
                        nextAttacking = Time.time + 1f / AttackDamage;
                        sword.Play();


                    }
                }
            
        }
    }

    void Attack()
    {
        

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, AttackRange, enemyLayers);
        foreach (Collider2D enemies in hitEnemies)
        {
            enemies.GetComponent<EnemyTakingDamage>().TakingDamage(AttackDamage);
           
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoint.position, AttackRange);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            InAir = false;
        }
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            InAir = true;
        }
    }
}
