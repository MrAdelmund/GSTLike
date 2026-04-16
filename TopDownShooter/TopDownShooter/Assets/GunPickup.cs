using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPickup : MonoBehaviour
{
    [Tooltip ("0 is normal gun, 1 is burst, 2 is fireball, 3 is seek")]
    public int gunIndex;
    [SerializeField] Sprite[] gunTypeSprites;
    void Start()
    {
        if (gunIndex > gunTypeSprites.Length)
        {
            Debug.LogWarning("Gun index is set to number outside bounds of array. Please fix, this will cause issues");
        }
        else
        {
            var gun1 = GetComponent<SpriteRenderer>();
            //sets the rendered sprited of object based off of assinged gun index
            gun1.sprite = gunTypeSprites[gunIndex];
        }
    }
}
