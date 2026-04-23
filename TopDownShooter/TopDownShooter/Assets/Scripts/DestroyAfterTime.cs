using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float timerToDestroy = 2.0f;
    void Start()
    {
        Destroy(gameObject, timerToDestroy);
    }
}
