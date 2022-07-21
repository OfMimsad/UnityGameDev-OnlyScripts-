using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public Transform player;
    private Rigidbody2D rb;
    private Animator anime;
    private Vector2 movement;

    public float moveSpeed = 5f;
    public float Distance;
    bool facingleft = true;
    bool moving = true;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

        
    void Update()
    {
        Vector3 direction = player.position - transform.position;
        direction.Normalize();
        movement = direction;
        
    }
    private void FixedUpdate()
    {
        moveCharacter(movement);
    }
    void moveCharacter(Vector2 direction)
    {
        if (Vector2.Distance(transform.position, player.position) >= Distance)
        {
            rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
            anime.SetBool("moving", moving);

        }else
        {    
            anime.SetBool("moving", false);
        }

        if (player.position.x >= gameObject.transform.position.x && facingleft)
        {
            Flip();
            

        } else if (player.position.x <= gameObject.transform.position.x && !facingleft )
        {
            Flip();
        }
    }

    void Flip()
    {
        Vector3 currrentScale = gameObject.transform.localScale;
        currrentScale.x *= -1;
        gameObject.transform.localScale = currrentScale;
        facingleft = !facingleft;

    }
   
}
