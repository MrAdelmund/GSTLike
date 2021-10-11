using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    public float jumpForce = 2.0f;
    Rigidbody2D rb;
    float startGravity;
    bool grounded;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startGravity = rb.gravityScale;
    }

    // Update is called once per frame
    private void Update()
    {
        if(rb.velocity.y < 0)
        {
            GetComponent<Rigidbody2D>().gravityScale = startGravity * 2.0f;
        }
        else
        {
            GetComponent<Rigidbody2D>().gravityScale = startGravity;
        }
        if (Input.GetButtonDown("Jump") && IsGrounded() && !Input.GetButton("Fire1"))
        {
            rb.AddForce(new Vector2(0, jumpForce * 50));

        }
    }
    void FixedUpdate()
    {
        //Debug.Log(IsGrounded());
        if (!Input.GetButton("Fire1") || !IsGrounded())
        {

            float x = Input.GetAxis("Horizontal");
            Vector2 vel = rb.velocity;
            vel.x = x * moveSpeed;
            rb.velocity = vel;
        }

    }
    public bool IsGrounded()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.55f * transform.lossyScale.y);
        Debug.DrawRay(transform.position, new Vector2(0, -0.55f * transform.lossyScale.y), Color.green);
        grounded = hit.collider != null && hit.collider.gameObject.name != gameObject.name;


        return grounded;
    }
}