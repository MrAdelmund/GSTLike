using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPickup : MonoBehaviour
{
    [Tooltip ("0 is normal gun, 1 is burst, 2 is fireball, 3 is seek")]
    public int gunIndex;
    public Sprite normal;
    public Sprite burst;
    public Sprite fire;
    public Sprite seek;
    // Start is called before the first frame update
    void Start()
    {
        var gun1 = GetComponent<SpriteRenderer>();
        if (gunIndex == 0)
        {
            gun1.sprite = normal;
        }
        if (gunIndex == 1)
        {
            gun1.sprite = burst;
        }
        if (gunIndex == 2)
        {
            gun1.sprite = fire;
        }
        if (gunIndex == 3)
        {
            gun1.sprite = seek;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
