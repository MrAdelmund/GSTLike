using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAtSetSpeed : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;
    void FixedUpdate()
    {
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z + rotationSpeed);
    }
}
