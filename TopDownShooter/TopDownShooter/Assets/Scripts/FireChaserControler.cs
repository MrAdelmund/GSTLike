using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireChaserControler : MonoBehaviour
{
    [SerializeField] Vector2[] PositionChain;
    [HideInInspector] public PlayerShoot shootScriptReference;
    Rigidbody2D rb;
    float bulletSpeed;
    Vector2 aimInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bulletSpeed = GetComponent<BulletData>().bulletSpeed;
    }
    void Update()
    {
        rb.velocity = (aimInput * bulletSpeed);
    }
}
