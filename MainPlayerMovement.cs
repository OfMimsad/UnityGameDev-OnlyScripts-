using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    
    [SerializeField] float playerSpeed;
    [SerializeField] float playerJumpForce;
    public HealthBarSCRIPTS healthBar;
    public MainPlayerAttack attack;
    float moveHorizontal;
    float moveVertical;
    bool isJumping;
    bool FacingRight = true;
    bool jump;
    float JumpingRate = 2;
    float nextJumping = 0f;
    public int maxHealth = 100;
    public int currentHealth;
    bool isdead = false;
    bool isAttacking;
    bool shild;

    
    [SerializeField] private AudioSource jumpingEffect;
    [SerializeField] private AudioSource dyingEffect;
    [SerializeField] private AudioSource takingDamage;
    [SerializeField] private AudioSource shield;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        healthBar.MaxHealth(maxHealth);
        attack = GetComponent<MainPlayerAttack>();
    }


    void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        anim.SetFloat("Walk", Mathf.Abs(moveHorizontal));
        anim.SetBool("Jumping", jump);
        anim.SetFloat("JumpF", rb.velocity.y);
      


        if (Input.GetKey(KeyCode.Mouse0))
        {
            isAttacking = true;
        } else
        {
            isAttacking = false;
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            shild = true;
            anim.SetBool("shild", shild);
            
        }
        else
        {
            shild = false;
            anim.SetBool("shild", shild);
           
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            shield.Play();
        }
    }
    private void FixedUpdate()
    {
        if (moveHorizontal != 0 && isAttacking == false && shild == false)
        {
            rb.AddForce(new Vector2(moveHorizontal * playerSpeed, 0f), ForceMode2D.Impulse);
            

        }

        

      if (Time.time >= nextJumping && shild == false) {
            if (moveVertical > 0.1 && isJumping == false)
            {
                rb.velocity = new Vector2(0f, moveVertical * playerJumpForce);
                isJumping = true;
                jump = true;
                nextJumping = Time.time + 1f / JumpingRate;
                jumpingEffect.Play();
            }
        }
        if (moveHorizontal > 0 && !FacingRight)
        {
            Flip();
        }
        if (moveHorizontal < 0 && FacingRight)
        {
            Flip();
        }

       

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if ( collision.gameObject.tag == "Ground"){
            isJumping = false;
            jump = false;
            
        }

    }

    void Flip()
    {
        Vector3 currrentScale = gameObject.transform.localScale;
        currrentScale.x *= -1;
        gameObject.transform.localScale = currrentScale;
        FacingRight = !FacingRight;
        

    }
    public void TakeDamage(int damage)
    {
        if (shild == false )
        {
            currentHealth -= damage;
            anim.SetTrigger("hurt");
            takingDamage.Play();
            healthBar.SetHealth(currentHealth);
        } else if (shild == true)
        { 
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
        }
        

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        anim.SetBool("IsDead", true);
        isdead = true;
        this.enabled = false;
        dyingEffect.Play();
        GetComponent<MainPlayerAttack>().enabled = false;



        StartCoroutine(AfterLife());
    }

    IEnumerator AfterLife()
    {
        if (isdead == true)
        {
            yield return new WaitForSecondsRealtime(1);
            Destroy(gameObject.GetComponent<Rigidbody2D>());
            GetComponent<CapsuleCollider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<MainPlayerAttack>().enabled = false;
        }

    }
  public bool CheckTheAttacking ()
    {
        return shild;
    }
    
}
