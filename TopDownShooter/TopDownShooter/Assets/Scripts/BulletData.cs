using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletData : MonoBehaviour
{
    public bool isLightsaber = false;
    public bool isChaserFire = false;
    public float bulletSpeed = 15;
    public float firerate = 0.1f;
    [SerializeField] bool destoryAfterDelay;
    [SerializeField] float destroyAfterTime = 2;
    private void Start()
    {
        if (destoryAfterDelay)
        {
            Destroy(gameObject, destroyAfterTime);
        }
    }
}
