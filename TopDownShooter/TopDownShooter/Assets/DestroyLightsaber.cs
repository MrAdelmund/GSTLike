using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLightsaber : MonoBehaviour
{
    float x = 0;
    float y = 0;
    // Start is called before the first frame update
    void Start()
    {
        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");
    }

    // Update is called once per frame
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
