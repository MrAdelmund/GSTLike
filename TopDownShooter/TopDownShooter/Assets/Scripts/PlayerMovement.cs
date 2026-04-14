using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 4.0f;
    public float jumpForce = 2.0f;
    [SerializeField] float extraFallMutiplyer = 2;
    [SerializeField] Vector2 groundCheckBoxCastSize = new Vector2(0.9f, 0.1f);
    [SerializeField] LayerMask groundLayer;
    Rigidbody2D rb;
    float defaultGravity;
    bool grounded;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
    }
    private void Update()
    {
        ExtraGravOnFall();
        if (Input.GetButtonDown("Jump") && IsGrounded() && !Input.GetButton("Fire1"))
        {
            rb.AddForce(new Vector2(0, jumpForce * 50));
        }
    }
    void FixedUpdate()
    {
        if (!Input.GetButton("Fire1") || !IsGrounded())
        {
            RunPlayerMovement();
        }
    }
    void RunPlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        Vector2 vel = rb.velocity;
        vel.x = x * moveSpeed;
        rb.velocity = vel;
    }
    void ExtraGravOnFall()
    {
        //If player is falling mutiply applied gravity by extraFallMutiplyer
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = defaultGravity * extraFallMutiplyer;
        }
        else
        {
            rb.gravityScale = defaultGravity;
        }
    }
    public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.55f * transform.lossyScale.y);
        //RaycastHit2D hit = Physics2D.BoxCast(transform.position, groundCheckBoxCastSize, 0, new Vector2 (0, 0.55f * transform.lossyScale.y), groundLayer);
        grounded = hit.collider != null && hit.collider.gameObject.name != gameObject.name;

        return grounded;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, new Vector2(0, -0.55f * transform.lossyScale.y));
        Gizmos.DrawWireCube(transform.position, groundCheckBoxCastSize);
    }
}