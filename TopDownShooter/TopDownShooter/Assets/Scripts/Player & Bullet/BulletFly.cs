using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    [SerializeField] BulletData bulletData;
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = (transform.up * bulletData.bulletSpeed);
    }
}