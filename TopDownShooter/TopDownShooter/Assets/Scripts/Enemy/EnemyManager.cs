using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4;
    [SerializeField] float jumpForce = 2;
    [SerializeField] ContactFilter2D groundCheckContactFilter;
    [Header("Enemy Behavior. Check Only 1")]
    [SerializeField] bool MeleeBehavior = true;
    [SerializeField] bool RangedBehavior;
    [Header("References")]
    [SerializeField] BoxCollider2D JumpCheckBoxL;
    [SerializeField] BoxCollider2D JumpCheckBoxR;
    bool isGrounded => rb.IsTouching(groundCheckContactFilter);
    Rigidbody2D rb;
    GameObject player;
    float defaultGravity;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        defaultGravity = rb.gravityScale;
    }
    void Update()
    {
        
    }
    public void JumpBoxTriggered(bool IsOnleft)
    {
        if (IsOnleft)
        {

        }else
        {

        }
    }
}
