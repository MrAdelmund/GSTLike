using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4.0f;
    [SerializeField] float jumpForce = 2.0f;
    [SerializeField] float extraFallMutiplyer = 2;
    [SerializeField] ContactFilter2D groundCheckContactFilter;
    bool isGrounded => rb.IsTouching(groundCheckContactFilter);
    Rigidbody2D rb;
    float defaultGravity;
    bool jumpPressed = false;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
    }
    private void Update()
    {
        //gets imput status for next fixed update call
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
    }
    void FixedUpdate()
    {
        RunPlayerMovement();
        AttemptJump();
        ExtraGravOnFall();
    }
    void RunPlayerMovement()
    {
        //allows player to move if they are not actively shooting
        if (!Input.GetButton("Fire1"))
        {
            float x = Input.GetAxis("Horizontal");
            Vector2 vel = rb.velocity;
            vel.x = x * moveSpeed;
            rb.velocity = vel;
        }
    }
    void AttemptJump()
    {
        //checks if player is able to jump
        if (jumpPressed && isGrounded && !Input.GetButton("Fire1"))
        {
            rb.AddForce(new Vector2(0, jumpForce * 50));
        }
        //resets jumpPressed for next check
        jumpPressed = false;
    }
    void ExtraGravOnFall()
    {
        //if player is falling mutiply applied gravity by extraFallMutiplyer
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = defaultGravity * extraFallMutiplyer;
        }
        else
        {
            rb.gravityScale = defaultGravity;
        }
    }
}