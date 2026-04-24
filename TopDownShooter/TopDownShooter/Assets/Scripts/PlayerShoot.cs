using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] Sprite[] gunSprites;
    [SerializeField] Image gun1;
    [SerializeField] Image gun2;
    [SerializeField] float shootDelay = 1.0f;
    [SerializeField] Combination[] gunCombos;
    public bool[] selectedGun1;
    public bool[] selectedGun2;
    public static bool pickedUpNewGun = false;
    float timer = 0;
    float facingDir = 0;
    int lastSelectedGun;
    Vector2 shootingDirection;
    bool lightsaberMode = false;
    GameObject prefab; //prefab that gets spawned in when shooting
    GameObject lightsaberObjectReference; //an object reference that is only needed when using the lightsaber
    int bullet1_ID = 0;
    int bullet2_ID = 0;
    //input variables
    bool firePressed = false;
    Vector2 aimInput;
    void Start()
    {
        facingDir = 1;
        lastSelectedGun = PickUpNewGun.selectedGun;
        //these two in this weird order is a funky work around to avoid having an
        //extra function that just gets used once in start to update both of the UI images.
        UpdateGun2();
        UpdateGun();
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
    void UpdateSprite(bool assignToGun1, int spriteToAssign)
    {
        //assigns sprites for ui based of provided index and wether to assign to gun1 or gun2 with provided bool
        if (assignToGun1)
            gun1.sprite = gunSprites[spriteToAssign];
        else
            gun2.sprite = gunSprites[spriteToAssign];
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
        //checks if the lightsaber weapon combo is still active, if not, it will exit the lightsaber state.
        if (prefab.name != "LightSaber" && lightsaberMode)
        {
            ExitLightsaberState();
        }
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
        float x = Mathf.RoundToInt(aimInput.x);
        float y = Mathf.RoundToInt(aimInput.y);
        //updates facing direction
        if (x != 0)
            facingDir = x;
        //Checks if there is player input, if not, shoot at y 0 and last faced x direction.
        if (x != 0|| y != 0)
            shootingDirection = new Vector2(x, y);
        else
            shootingDirection = new Vector2(facingDir, 0);
        //updates shooting direction
        transform.localPosition = shootingDirection;
    }
    void Shoot()
    {
        if (firePressed && !lightsaberMode)
        {
            GameObject bullet = Instantiate(prefab, transform.position, Quaternion.identity);
            if (prefab.name == "fire" || prefab.name == "StrongFire")
            {
                bullet.GetComponent<BulletFlyWithParent>().parentRB = transform.parent.GetComponent<Rigidbody2D>();
            }
            bullet.transform.up = shootingDirection;
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
                ExitLightsaberState();
            }
            //updates the curent spawned in lightsaber's rotation and position
            lightsaberObjectReference.transform.up = shootingDirection;
            lightsaberObjectReference.transform.position = transform.position;
        }
    }
    void ExitLightsaberState()
    {
        lightsaberMode = false;
        Destroy(lightsaberObjectReference);
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
public class Combination
{
    public GameObject[] bullets;
}