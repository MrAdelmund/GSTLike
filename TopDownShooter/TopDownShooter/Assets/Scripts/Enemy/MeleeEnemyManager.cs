using UnityEngine;

public class MeleeEnemyManager : MonoBehaviour
{
    [Header("Melee Enemy Config")]
    [SerializeField] float moveSpeed = 4;
    [SerializeField] float jumpForce = 15;
    [SerializeField] ContactFilter2D groundCheckContactFilter;
    [Header("References")]
    [SerializeField] BoxCollider2D JumpCheckBoxL;
    [SerializeField] BoxCollider2D JumpCheckBoxR;
    Animator animator;
    SpriteRenderer spr;
    Rigidbody2D rb;
    GameObject player;
    bool isGrounded => rb.IsTouching(groundCheckContactFilter);
    bool properlyStandingStill;
    int inproperlyStandingStillFor;
    float stopMovingTimer = 0;
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
    }
    void FixedUpdate()
    {
        if (stopMovingTimer <= 0)
        {
            RunMovement();
        }
        if (Vector2.Distance(player.transform.position, transform.position) < 1)
        {
            StopMovingFor(1);
            StartAttack();
            rb.velocity = Vector2.zero;
        }
        //updates values for animaior
        animator.SetBool("Grounded", isGrounded);
        animator.SetBool("Moving", (rb.velocity.x != 0) ? true : false);
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
            properlyStandingStill = false;
        }
        else if (player.transform.position.x - transform.position.x < -0.05)
        {
            movedir = -1;
            spr.flipX = false;
            properlyStandingStill = false;
        }
        else
            properlyStandingStill = true;
        rb.velocity = new Vector2(movedir * moveSpeed, rb.velocity.y);
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
        animator.SetBool("Attacking", true);
    }
    //this is called by an animation event
    public void Attack()
    {
        animator.SetBool("Attacking", false);
    }
}
