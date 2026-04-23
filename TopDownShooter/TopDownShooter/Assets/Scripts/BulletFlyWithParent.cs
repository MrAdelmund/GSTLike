using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFlyWithParent : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 15.0f;
    [HideInInspector] public Rigidbody2D parentRB;
    Vector2 finalBulletSpeed;
    Rigidbody2D rb;
    void Start()
    {
        finalBulletSpeed = transform.up * bulletSpeed;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = finalBulletSpeed;
    }
    private void Update()
    {
        rb.velocity = (parentRB.velocity + finalBulletSpeed);
    }
}