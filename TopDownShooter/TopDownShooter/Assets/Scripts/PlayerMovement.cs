using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
    bool firePressed = false;
    float moveInputX;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
    }
    void FixedUpdate()
    {
        RunPlayerMovement();
        AttemptJump();
        ExtraGravOnFall();
    }
    public void RunPlayerMovement()
    {
        //allows player to move if they are not actively shooting
        if (!firePressed)
        {
            Vector2 vel = rb.velocity;
            if (moveInputX == 0)
            {
                vel.x = 0;
            } else if (moveInputX > 0)
            {
                vel.x = moveSpeed;
            } else if (moveInputX < 0)
            {
                vel.x = -moveSpeed;
            }
            rb.velocity = vel;
        }else if (firePressed && isGrounded)
        {
            //Quickly diminishes speed when the player starts shooting while
            //walking on the ground, so they don't slide as much, but also don't suddenly stop.
            rb.velocity = new Vector2(rb.velocity.x * 0.8f, 0);
        }
    }
    public void AttemptJump()
    {
        //checks if player is able to jump
        if (jumpPressed && isGrounded && !firePressed)
        {
            rb.AddForce(new Vector2(0, jumpForce * 50));
        }
        //resets jumpPressed to stop repeat tirggers
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
    //updaters for inputs
    public void PlayerInputMove(InputAction.CallbackContext context)
    {
        moveInputX = context.ReadValue<Vector2>().x;
    }
    public void PlayerInputJump(InputAction.CallbackContext context)
    {
        jumpPressed = context.performed;
    }
    public void PlayerInputFire(InputAction.CallbackContext context)
    {
        firePressed = context.performed;
    }
}