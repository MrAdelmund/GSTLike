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
    public Sprite normal;
    public Sprite burst;
    public Sprite fire;
    public Sprite seek;
    /*public GameObject normal;
    public GameObject burst;
    public GameObject seek;
    public GameObject fire;
    public GameObject strongBurst;
    public GameObject strongNormal;
    public GameObject strongSeek;
    public GameObject fireBall;*/
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
    // Start is called before the first frame update
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
            if (bullet1[i] != 0)
            {
                bullet1_ID = i;
                bullet2_ID = 0;
                if (i == 0)
                {
                    gun1.sprite = normal;
                }
                if (i == 1)
                {
                    gun1.sprite = burst;
                }
                if (i == 2)
                {
                    gun1.sprite = fire;
                }
                if (i == 3)
                {
                    gun1.sprite = seek;
                }
                break;
            }
        }
        for (int i = 0; i < bullet2.Length; i++)
        {
            if (bullet2[i] != 0)
            {
                bullet2_ID = i;
                bullet2_ID++;
                if (i == 0)
                {
                    gun2.sprite = normal;
                }
                if (i == 1)
                {
                    gun2.sprite = burst;
                }
                if (i == 2)
                {
                    gun2.sprite = fire;
                }
                if (i == 3)
                {
                    gun2.sprite = seek;
                }
                break;
            }
        }
    }
    private void UpdateGun()
    {
        int bullet1_ID = 0;
        int bullet2_ID = 0;
        if (PickUpNewGun.selectedGun == 0)
        {
            for (int i = 0; i < bullet1.Length; i++)
            {
                if (bullet1[i] != 0)
                {
                    bullet1_ID = i;
                    bullet2_ID = 0;
                    if (i == 0)
                    {
                        gun1.sprite = normal;
                    }
                    if (i == 1)
                    {
                        gun1.sprite = burst;
                    }
                    if (i == 2)
                    {
                        gun1.sprite = fire;
                    }
                    if (i == 3)
                    {
                        gun1.sprite = seek;
                    }
                    break;
                }
            }
        }if(PickUpNewGun.selectedGun == 1)
        {
            for (int i = 0; i < bullet2.Length; i++)
            {
                if (bullet2[i] != 0)
                {
                    bullet2_ID = i;
                    if (i == 0)
                    {
                        gun2.sprite = normal;
                    }
                    if (i == 1)
                    {
                        gun2.sprite = burst;
                    }
                    if (i == 2)
                    {
                        gun2.sprite = fire;
                    }
                    if (i == 3)
                    {
                        gun2.sprite = seek;
                    }
                    break;
                }
            }
            bullet1_ID = bullet2_ID;
            bullet2_ID = 0;
        }
        if(PickUpNewGun.selectedGun == 2)
        {
            for (int i = 0; i < bullet1.Length; i++)
            {
                if (bullet1[i] != 0)
                {
                    bullet1_ID = i;
                    bullet2_ID = 0;
                    if (i == 0)
                    {
                        gun1.sprite = normal;
                    }
                    if (i == 1)
                    {
                        gun1.sprite = burst;
                    }
                    if (i == 2)
                    {
                        gun1.sprite = fire;
                    }
                    if (i == 3)
                    {
                        gun1.sprite = seek;
                    }
                    break;
                }
            }
            for (int i = 0; i < bullet2.Length; i++)
            {
                if (bullet2[i] != 0)
                {
                    bullet2_ID = i;
                    bullet2_ID++;
                    if (i == 0)
                    {
                        gun2.sprite = normal;
                    }
                    if (i == 1)
                    {
                        gun2.sprite = burst;
                    }
                    if (i == 2)
                    {
                        gun2.sprite = fire;
                    }
                    if (i == 3)
                    {
                        gun2.sprite = seek;
                    }
                    break;
                }
            }
        }
        
       
        
        prefab = gunCombos[bullet1_ID].bullets[bullet2_ID];
        /*
        if (bullet2_ID < 0)
        {
            switch (bullet1_ID)
            {

                case 0:
                    gunType = "normal";
                    break;
                case 1:
                    gunType = "pulse";
                    break;
                case 3:
                    gunType = "seek";
                    break;
                case 4:
                    gunType = "fire";
                    break;

            }
        }
        if (bullet2_ID == 0)
        {
            switch (bullet1_ID)
            {

                case 0:
                    gunType = "strongNormal";
                    break;
                case 1:
                    gunType = "strongPulse";
                    break;
                case 3:
                    gunType = "strongSeek";
                    break;
                case 4:
                    gunType = "fireBalls";
                    break;

            }
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGun();
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
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
        timer += Time.deltaTime;
        if (Input.GetButton("Fire1") && timer > shootDelay)
        {
            
                
                //bulletLifetime = 0.5f;
                //shootDelay = 0.2f;
                timer = 0;
                GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
                Vector2 shootDir;
                if (y != 0)
                    shootDir = new Vector2(x, y);
                else
                    shootDir = new Vector2(facingDir, 0);
                //Vector2 shootDir = new Vector2(mousePosition.x - transform.position.x,
                //    mousePosition.y - transform.position.y);
                shootDir.Normalize();
                //bullet.GetComponent<Rigidbody2D>().velocity = shootDir * bulletSpeed;
                bullet.transform.up = shootDir;
                //Destroy(bullet, bulletLifetime);
            
            /*if (gunType == "pulse")
            {
                bulletLifetime = 0.5f;
                shootDelay = 0.2f;
                timer = 0;
                Debug.Log(transform.position);
                GameObject bullet = Instantiate(burst, transform.position, Quaternion.identity);
                Vector2 shootDir;
                if (y != 0)
                    shootDir = new Vector2(x, y);
                else
                    shootDir = new Vector2(facingDir, 0);

                shootDir.Normalize();
                bullet.transform.up = shootDir;
                Destroy(bullet, bulletLifetime);


            }
            if (gunType == "seek")
            {
                bulletLifetime = 10.5f;
                shootDelay = 0.2f;
                timer = 0;
                Debug.Log(transform.position);
                GameObject bullet = Instantiate(seek, transform.position, Quaternion.identity);
                Vector2 shootDir;
                if (y != 0)
                    shootDir = new Vector2(x, y);
                else
                    shootDir = new Vector2(facingDir, 0);

                shootDir.Normalize();
                bullet.transform.up = shootDir;
                Destroy(bullet, bulletLifetime);


            }*/
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


