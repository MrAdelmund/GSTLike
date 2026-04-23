using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLightsaber : MonoBehaviour
{
    float x = 0;
    float y = 0;
    void Start()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }
    void Update()
    {
        float tempx = Input.GetAxisRaw("Horizontal");
        float tempy = Input.GetAxisRaw("Vertical");
        if(tempx != x || tempy != y || !Input.GetButton("Fire1"))
        {
            Destroy(gameObject);
        }
    }
}
