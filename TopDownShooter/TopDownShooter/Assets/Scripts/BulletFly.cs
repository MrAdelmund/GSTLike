using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFly : MonoBehaviour
{
    public float bulletSpeed = 15.0f;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
