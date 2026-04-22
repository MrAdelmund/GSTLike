using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    public bool[] selectedGun1;
    public bool[] selectedGun2;
    public string gunType = "normal";
    public Combination[] gunCombos;
    public static bool pickedUpNewGun = false;
    int lastSelectedGun;
    Vector2 aimInput;
    Vector2 shootingDirection;
    bool firePressed = false;
    bool lightsaberMode = false;
    GameObject lightsaberObjectReference;
    int bullet1_ID = 0;
    int bullet2_ID = 0;
    void Start()
    {
        facingDir = 1;
        startX = transform.localPosition.x;
        startY = transform.localPosition.y;
        lastSelectedGun = PickUpNewGun.selectedGun;
        UpdateGun();
        UpdateImages();
    }
    void Update()
    {
        //update gun being used and sprites when selected gun is changed/a new gun is picked up.
        if (PickUpNewGun.selectedGun != lastSelectedGun || pickedUpNewGun)
        {
            //sets values for next check
            lastSelectedGun = PickUpNewGun.selectedGun;
            pickedUpNewGun = false;
            //updated gun & sprites
            UpdateGun();
        }
        TryShooting();
    }
    //UpdateImages is only called at start, all it does is update both images
    void UpdateImages()
    {
        int bullet1_ID = -1;
        int bullet2_ID = -1;
        //Goes through each value in bullet1[] until it reachs a value that is true.
        //If a value is true that means that is the gun that is selected.
        for (int i = 0; i < selectedGun1.Length; i++)
        {
            if (selectedGun1[i])
            {
                bullet1_ID = i;
                bullet2_ID = 0;
                //sets sprite based off index
                UpdateSprite(true, i);
                break;
            }
        }
        for (int i = 0; i < selectedGun2.Length; i++)
        {
            if (selectedGun2[i])
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
    //sets the bullet prefab to use based off currently selected gun(s)
    private void UpdateGun()
    {
        bullet1_ID = 0;
        bullet2_ID = 0;
        //For gun slot 1
        if (PickUpNewGun.selectedGun == 0)
        {
            UpdateGun1();
        }
        //For gun slot 2
        if(PickUpNewGun.selectedGun == 1)
        {
            UpdateGun2();
            bullet1_ID = bullet2_ID;
            bullet2_ID = 0;
        }
        //For both gun slots
        if(PickUpNewGun.selectedGun == 2)
        {
            UpdateGun1();
            UpdateGun2();
        }
        //assigns prefab to use based off buttlet 1 & 2 Id intex
        prefab = gunCombos[bullet1_ID].bullets[bullet2_ID];
    }
    void UpdateGun1()
    {
        for (int i = 0; i < selectedGun1.Length; i++)
        {
            if (selectedGun1[i])
            {
                bullet1_ID = i;
                bullet2_ID = 0;
                UpdateSprite(true, i);
                break;
            }
        }
    }
    void UpdateGun2()
    {
        for (int i = 0; i < selectedGun2.Length; i++)
        {
            if (selectedGun2[i])
            {
                bullet2_ID = i;
                if (PickUpNewGun.selectedGun == 2)
                    bullet2_ID++;
                UpdateSprite(false, i);
                break;
            }
        }
    }
    void TryShooting()
    {
        //shoot delay
        timer += Time.deltaTime;
        if (timer > shootDelay || lightsaberMode)
        {
            timer = 0;
            UpdateShootingDirection();
            Shoot();
        }
    }
    void UpdateShootingDirection()
    {
        //gets imputs for calcuations
        float x = aimInput.x;
        float y = aimInput.y;
        Debug.Log(aimInput);
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
        if (x != 0 && y != 0)
        {
            Vector3 pos = transform.localPosition;
            pos.y = y * 0.45f;
            pos.x = x * startX * 0.75f;
            transform.localPosition = pos;
        }
        if (x == 0 && y == 0)
        {
            Vector3 pos = transform.localPosition;
            pos.y = startY;
            pos.x = facingDir * startX;
            transform.localPosition = pos;
        }
        shootingDirection = new Vector2(x, y);
    }
    void UpdateShootingDirection2ElectricBoogaloo()
    {
        float x = aimInput.x;
        float y = aimInput.y;

    }
    void Shoot()
    {
        if (firePressed && !lightsaberMode)
        {
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            bullet.transform.up = CalcBulletOrientation();
            //checks if the spawned prefab is a LightSaber
            if (prefab.name == "LightSaber")
            {
                //"An elegant weapon for a more civilized time" - Obi-Wan Kenobi
                lightsaberMode = true;
                lightsaberObjectReference = bullet;
            }

        }
        //does specific Lightsaber behavior if in lightsaber mode
        else if (lightsaberMode == true)
        {
            //  READ ME
            //the Lightsaber weapon has a different behavior compaired to other weapons
            //the Lightsaber is a persisting projectile and just has it's rotation & position updated

            //destorys Lightsaber if fire button is not pressed
            if (!firePressed)
            {
                lightsaberMode = false;
                Destroy(lightsaberObjectReference);
            }
            //updates the curent spawned in lightsaber's rotation and position
            lightsaberObjectReference.transform.up = CalcBulletOrientation();
            lightsaberObjectReference.transform.position = transform.position;
        }
    }
    Vector2 CalcBulletOrientation()
    {
        //calculates the orientation/direction that the bullet will be facing
        Vector2 shootDir;
        if (shootingDirection.y != 0)
            shootDir = new Vector2(shootingDirection.x, shootingDirection.y);
        else
            shootDir = new Vector2(facingDir, 0);
        shootDir.Normalize();
        return shootDir;
    }
    //updaters for inputs
    public void PlayerInputAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }
    public void PlayerInputFire(InputAction.CallbackContext context)
    {
        firePressed = context.performed;
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