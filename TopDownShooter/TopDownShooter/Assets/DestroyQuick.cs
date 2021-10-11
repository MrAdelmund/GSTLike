using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyQuick : MonoBehaviour
{
    public float timerToDestroy = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timerToDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
