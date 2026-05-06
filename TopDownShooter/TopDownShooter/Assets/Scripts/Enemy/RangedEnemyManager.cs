using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyManager : MonoBehaviour
{
    [Header("Ranged Enemy Config")]
    [SerializeField] float moveSpeed = 4;
    [SerializeField] float jumpForce = 15;
    [SerializeField] float attackCooldown = 1.5f;
    [SerializeField] float fleeDistance = 6;
    [SerializeField] float followDistance = 10;
    [SerializeField] ContactFilter2D groundCheckContactFilter;
    [Header("References")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firePointTrans;
    [SerializeField] BoxCollider2D jumpCheckBoxL;
    [SerializeField] BoxCollider2D jumpCheckBoxR;
    Animator animator;
    SpriteRenderer spr;
    Rigidbody2D rb;
    GameObject player;
    bool isGrounded => rb.IsTouching(groundCheckContactFilter);
    bool properlyStandingStill;
    int inproperlyStandingStillFor;
    float shootDir = 0;
    float stopMovingTimer = 0;
    float cantAttackFor = 0;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponentInChildren<Animator>();
        spr = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        stopMovingTimer -= Time.deltaTime;
        cantAttackFor -= Time.deltaTime;
    }
    void FixedUpdate()
    {
        if (stopMovingTimer <= 0)
        {
            RunMovement();
        }
        //starts an attack when conditions are met
        if (properlyStandingStill && cantAttackFor <= 0)
        {
            StopMovingFor(1);
            StartAttack();
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
        //updates values for animaior
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("Moving", (rb.velocity.x >= 0.05 || rb.velocity.x <= -0.05) ? true : false);
    }
    void RunMovement()
    {
        //will make the enemy jump if they get stuck not moving when they should be
        if (!properlyStandingStill && rb.velocity == Vector2.zero)
        {
            inproperlyStandingStillFor++;
            if (inproperlyStandingStillFor > 10)
            {
                inproperlyStandingStillFor = 0;
                Jump();
            }
        }else
        {
            inproperlyStandingStillFor = 0;
        }
        //sets the movement for the enemy based off player position
        int movedir = 0;
        if (player.transform.position.x - transform.position.x > 0.05)
        {
            movedir = 1;
            spr.flipX = true;
            shootDir = 1;
            properlyStandingStill = false;
        }
        else if (player.transform.position.x - transform.position.x < -0.05)
        {
            movedir = -1;
            spr.flipX = false;
            shootDir = -1;
            properlyStandingStill = false;
        }
        else
            properlyStandingStill = true;
        float playerDist = Vector2.Distance(player.transform.position, transform.position);
        if (playerDist >= followDistance)
        {
            rb.velocity = new Vector2(movedir * moveSpeed, rb.velocity.y);
            properlyStandingStill = false;
        }
        else if (playerDist <= fleeDistance)
        {
            rb.velocity = new Vector2((-1 * movedir) * moveSpeed, rb.velocity.y);
            properlyStandingStill = false;
            spr.flipX = !spr.flipX;
        }
        else
            properlyStandingStill = true;
    }
    //when called, will stop enemy from moving for (time)
    void StopMovingFor(float time)
    {
        stopMovingTimer = time;
    }
    //this gets triggered by the JumpBoxTirggerer script
    public void JumpBoxTriggered(bool isOnleft)
    {
        if (isOnleft && isGrounded && !spr.flipX)
            Jump();
        else if (!isOnleft && isGrounded && spr.flipX)
            Jump();
    }
    void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce * 50));
    }
    void StartAttack()
    {
        cantAttackFor = attackCooldown;
        animator.SetBool("Attacking", true);
        
    }
    //this is called by an animation event on the enemie's sprite object
    public void Attack()
    {
        animator.SetBool("Attacking", false);
        GameObject bullet = Instantiate(bulletPrefab, firePointTrans.position, firePointTrans.rotation);
        bullet.transform.up = new Vector2(shootDir, 0);
    }
}
