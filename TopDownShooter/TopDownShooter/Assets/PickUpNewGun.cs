using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PickUpNewGun : MonoBehaviour
{
    public static int selectedGun = 0;
    [SerializeField] GameObject GunPickupPrefab;
    [SerializeField] Image gun1;
    [SerializeField] Image gun2;
    //input variables
    bool gunSelectPressed = false;
    bool pickupGunPressed = false;

    void Start()
    {
        gun1.enabled = true;
        gun2.enabled = false;
    }
    void Update()
    {
        if (gunSelectPressed)
        {
            gunSelectPressed = false;
            selectedGun++;
            selectedGun %= 3;
            if(selectedGun == 0)
            {
                gun1.enabled = true;
                gun2.enabled = false;
            }
            else if (selectedGun == 1)
            {
                gun1.enabled = false;
                gun2.enabled = true;
            }
            else
            {
                gun1.enabled = true;
                gun2.enabled = true;
            }
        }
    
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        int oldGunID = -1;
        if (pickupGunPressed && collision.gameObject.GetComponent<GunPickup>() != null)
        {
            pickupGunPressed = false;
            int gunID = collision.gameObject.GetComponent<GunPickup>().gunIndex;
            //if the selected gun is index 0 or 2 (slot 1 or both) switch out the first gun slot
            if(selectedGun == 0 || selectedGun == 2)
            {
                bool[] temp = GetComponentInChildren<PlayerShoot>().selectedGun1;
                for(int i = 0; i < temp.Length; i++)
                {
                    if (temp[i])
                    {
                        oldGunID = i;
                        temp[i] = false;
                        break;
                    }
                }
                temp[gunID] = true;
            }
            //if the selected gun is index 1 switch out the second gun slot
            if (selectedGun == 1)
            {

                bool[] temp = GetComponentInChildren<PlayerShoot>().selectedGun2;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i])
                    {
                        oldGunID = i;
                        temp[i] = false;
                        break;
                    }
                }
                temp[gunID] = true;
            }
            if (oldGunID > -1)
            {
                Vector3 pos = collision.gameObject.transform.position;
                pos.y += 1;
                GameObject obj = Instantiate(GunPickupPrefab, pos, Quaternion.identity);
                //Debug.Log("Old gun ID: " + oldGunID);
                obj.GetComponent<GunPickup>().gunIndex = oldGunID;
                Destroy(collision.gameObject);
            }
            PlayerShoot.pickedUpNewGun = true;
            Destroy(collision.gameObject);
        }
    }
    //updaters for inputs
    public void PlayerInputToggleGun(InputAction.CallbackContext context)
    {
        gunSelectPressed = context.performed;
    }
    public void PlayerInputPickupGun(InputAction.CallbackContext context)
    {
        pickupGunPressed = context.performed;
    }
}
