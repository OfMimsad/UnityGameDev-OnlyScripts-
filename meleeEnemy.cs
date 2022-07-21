using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private int shilddamage;
    private float coolDownTimer;
    Animator anim;
    private MainPlayerMovement playerhealth;
    bool isdead;
    bool shild;
    public Transform player;
    PlayerShieldInSight enemyinSight;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyinSight = GameObject.Find("EnemyDetector").GetComponent<PlayerShieldInSight>();

    }
    private void Update()
    {
        
        coolDownTimer += Time.deltaTime;

        if (PlayerInSight() && (playerhealth.currentHealth > 0))
        {
            if (coolDownTimer >= attackCooldown)
            {
                coolDownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            shild = true;

        }
        else
        {
            shild = false;
        }
     

        if (enemyinSight.EnemyINsight() && shild == true)
        {
             shilddamage = 0;
            damage = shilddamage;
        }
        else if (shild == false || !enemyinSight.EnemyINsight())
        {
            shilddamage = 10;
            damage = shilddamage;
        }
        
    }

    bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range , boxCollider.bounds.size.y, boxCollider.bounds.size.z),0,Vector2.left,0, playerLayer );
        if (hit.collider != null)
        {
            playerhealth = hit.transform.GetComponent<MainPlayerMovement>();

        }

        return hit.collider != null;

    }
        private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }
    public void PlayerDamage()
    {
        
        if (PlayerInSight() && (playerhealth.currentHealth > 0))
        { 
            
            playerhealth.TakeDamage(damage);
           

        }
    }
    
  
}
