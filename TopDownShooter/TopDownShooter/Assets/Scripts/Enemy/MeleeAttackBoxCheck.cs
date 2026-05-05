using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackBoxCheck : MonoBehaviour
{
    [SerializeField] int attackDammage = 7;
    [SerializeField] float timeToAttemptDammage = 0.1f;
    [SerializeField] SpriteRenderer enemySpriteRenderer;
    [HideInInspector] public float currentAttemptedDammageTime = 0;
    float boxColliderDefaultXOffset = 0;
    MeleeEnemyManager meleeEnemyManager;
    BoxCollider2D boxCollider;
    private void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        meleeEnemyManager = GetComponentInParent<MeleeEnemyManager>();
        //gets the inital position of the boxcolliders x offset
        boxColliderDefaultXOffset = boxCollider.offset.x;
    }
    private void Update()
    {
        //ticks timer
        currentAttemptedDammageTime += Time.deltaTime;
        //checks if object should be active based off timer
        if (currentAttemptedDammageTime >= timeToAttemptDammage)
            gameObject.SetActive(false);
        //sets the position of the box based off the flipx value of the sprite renderer
        if (enemySpriteRenderer.flipX)
            boxCollider.offset = new Vector2(boxColliderDefaultXOffset * -1, boxCollider.offset.y);
        else
            boxCollider.offset = new Vector2(boxColliderDefaultXOffset, boxCollider.offset.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checks if it collided with a player and applies dammage accordingly
        if (collision.GetComponent<PlayerHealth>() != null)
        {
            collision.GetComponent<PlayerHealth>().TakeDammage(attackDammage);
            meleeEnemyManager.AttackHit();
        }
    }
}
