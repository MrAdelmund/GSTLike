using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeekEnemies : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] bool instant = false;
    [SerializeField] float turnSpeed = 1f;
    float dist = 10000;
    float timer;
    void Start()
    {
        FindTarget();
    }
    private void Update()
    {
        timer += Time.deltaTime;
    }
    void FixedUpdate()
    {
        if(target == null)
        {
            FindTarget();
        }
        else
        {
            if (instant)
            {
                transform.position = target.transform.position;
                GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            }
            else
            {
                transform.up = Vector3.Lerp(transform.up, target.transform.position - transform.position, 0.2f * timer * turnSpeed);//0.053f);
                float speed = GetComponent<Rigidbody2D>().velocity.magnitude;
                GetComponent<Rigidbody2D>().velocity = transform.up * speed;
            }
        }
    }
    void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            if (Vector3.Distance(transform.position, enemies[i].transform.position) < dist)
            {
                target = enemies[i];
                dist = Vector3.Distance(transform.position, enemies[i].transform.position);
            }
        }
    }
}
