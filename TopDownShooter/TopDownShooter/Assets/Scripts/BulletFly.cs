using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    public float bulletSpeed = 15.0f;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = (transform.up * bulletSpeed);
    }
}