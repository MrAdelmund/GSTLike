using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    [SerializeField] GameObject HitExplodePrefab;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        if (HitExplodePrefab != null)
            Instantiate(HitExplodePrefab, transform.position, transform.rotation);
    }
}
