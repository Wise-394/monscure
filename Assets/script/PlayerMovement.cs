using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed;
    public Animator anim;

    private Vector2 moveDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        inputs();
    }
    void FixedUpdate()
    {
        move();
    }
    void inputs() 
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;
        if (moveDirection != Vector2.zero)
        {
            
            anim.SetBool("running", true);
        }
        else
        {
            anim.SetBool("running", false);
        }

    }
    void move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed , moveDirection.y * moveSpeed);
    }

}
