using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletDamage : MonoBehaviour
{
    int damage;
    private void Start()
    {
        BulletData bulletData = GetComponent<BulletData>();
        if (bulletData == null)
            bulletData = GetComponentInParent<BulletData>();
        damage = bulletData.bulletDamage;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}