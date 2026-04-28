using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCounterRotation : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.eulerAngles = Vector3.zero;
    }
}