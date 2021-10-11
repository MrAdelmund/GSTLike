using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtMouse : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Input.mousePosition;
        Debug.Log("Mouse position 1: " + mousePosition);
        //convert x,y pixels to game world position
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        mousePosition.z = transform.position.z;
        Vector3 dir = mousePosition - transform.position;
        transform.up = dir;// Vector3.Lerp(transform.right, mousePosition - transform.position, 1.0f);
        
    }
}
