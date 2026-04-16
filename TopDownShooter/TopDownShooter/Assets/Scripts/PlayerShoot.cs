using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerShoot : MonoBehaviour
{
    public GameObject prefab;
    public Image gun1;
    public Image gun2;
    [SerializeField] Sprite[] gunSprites;
    public float bulletSpeed = 10.0f;
    public float bulletLifetime = 1.0f;
    public float shootDelay = 1.0f;
    float timer = 0;
    float facingDir = 0;
    float startX;
    float startY;
    public int[] bullet1;
    public int[] bullet2;
    public string gunType = "normal";
    public Combination[] gunCombos;
    void Start()
    {
        facingDir = 1;
        startX = transform.localPosition.x;
        startY = transform.localPosition.y;
        UpdateGun();
        UpdateImages();
    }
    void UpdateImages()
    {
        int bullet1_ID = -1;
        int bullet2_ID = -1;
        for (int i = 0; i < bullet1.Length; i++)
        {
            //if the selected gun in bullet1[]
            if (bullet1[i] != 0)
            {
                bullet1_ID = i;
                bullet2_ID = 0;
                //sets sprite based off index
                UpdateSprite(true, i);
                break;
            }
        }
        for (int i = 0; i < bullet2.Length; i++)
        {
            if (bullet2[i] != 0)
            {
                bullet2_ID = i;
                bullet2_ID++;
                UpdateSprite(false, i);
                break;
            }
        }
    }
    void UpdateSprite(bool assignToGun1, int spriteToAssign)
    {
        //assigns sprites for ui based of provided index and weather to assign to gun1 or gun2 with provided bool
        if (assignToGun1)
        {
            gun1.sprite = gunSprites[spriteToAssign];
        } else
        {
            gun2.sprite = gunSprites[spriteToAssign];
        }
    }
    private void UpdateGun()
    {
        int bullet1_ID = 0;
        int bullet2_ID = 0;

        //For gun slot 1
        if (PickUpNewGun.selectedGun == 0)
        {
            for (int i = 0; i < bullet1.Length; i++)
            {
                if (bullet1[i] != 0)
                {
                    bullet1_ID = i;
                    bullet2_ID = 0;
                    UpdateSprite(true, i);
                    break;
                }
            }
        }
        //For gun slot 2
        if(PickUpNewGun.selectedGun == 1)
        {
            for (int i = 0; i < bullet2.Length; i++)
            {
                if (bullet2[i] != 0)
                {
                    bullet2_ID = i;
                    UpdateSprite(false, i);
                    break;
                }
            }
            bullet1_ID = bullet2_ID;
            bullet2_ID = 0;
        }
        //For both gun slots
        if(PickUpNewGun.selectedGun == 2)
        {
            for (int i = 0; i < bullet1.Length; i++)
            {
                if (bullet1[i] != 0)
                {
                    bullet1_ID = i;
                    bullet2_ID = 0;
                    UpdateSprite(true, i);
                    break;
                }
            }
            for (int i = 0; i < bullet2.Length; i++)
            {
                if (bullet2[i] != 0)
                {
                    bullet2_ID = i;
                    bullet2_ID++;
                    UpdateSprite(false, i);
                    break;
                }
            }
        }

        //assigns prefab to use based off buttlet 1 & 2 Id intex
        prefab = gunCombos[bullet1_ID].bullets[bullet2_ID];
    }
    void Update()
    {
        UpdateGun();
        //gets imputs for calcuations
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //calculates facing/shooting direction
        if (x != 0)
        {
            facingDir = x;

            Vector3 pos = transform.localPosition;
            pos.x = x * startX;
            pos.y = startY;
            transform.localPosition = pos;
        }
        if (x == 0 && y != 0)
        {

            Vector3 pos = transform.localPosition;
            pos.y = y * 0.75f;
            pos.x = 0;
            
            transform.localPosition = pos;

        }
        if(x != 0 && y != 0)
        {
            Vector3 pos = transform.localPosition;
            pos.y = y * 0.45f;
            pos.x = x * startX * 0.75f;
            transform.localPosition = pos;
        }
        if(x == 0 && y == 0)
        {
            Vector3 pos = transform.localPosition;
            pos.y = startY;
            pos.x = facingDir * startX;
            transform.localPosition = pos;
        }
        //shoot delay
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer > shootDelay)
        {
                timer = 0;
                GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
                Vector2 shootDir;
                if (y != 0)
                    shootDir = new Vector2(x, y);
                else
                    shootDir = new Vector2(facingDir, 0);
                shootDir.Normalize();
                bullet.transform.up = shootDir;
        }
    }
}
[System.Serializable]
public class Bullet
{
    public GameObject prefab;
    public float bulletLifetime;
    public float shootDelay;

}
[System.Serializable]
public class Combination
{
    public GameObject[] bullets;
}