using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnHit : MonoBehaviour
{
    [SerializeField] GameObject prefabToSpawn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Instantiate(prefabToSpawn, transform.position, Quaternion.Euler(Vector3.zero));
        Destroy(gameObject); 
    }
}
