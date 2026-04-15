using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpNewGun : MonoBehaviour
{
    public static int selectedGun = 0;
    public GameObject GunPickupPrefab;
    public Image gun1;
    public Image gun2;
    void Start()
    {
        gun1.enabled = true;
        gun2.enabled = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("ToggleGunSelect"))
        {
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
        //float y = Input.GetAxisRaw("Vertical");
        int oldGunID = -1;
        if (/*y < 0 &&*/ Input.GetButton("SwapGun") && collision.gameObject.GetComponent<GunPickup>() != null)
        {
            
            int gunID = collision.gameObject.GetComponent<GunPickup>().gunIndex;
            //if the selected gun is index 0 or 2 (slot 1 or both) switch out the first gun slot
            if(selectedGun == 0 || selectedGun == 2)
            {
                
                int[] temp = GetComponentInChildren<PlayerShoot>().bullet1;
                for(int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] != 0)
                    {
                        oldGunID = i;
                        temp[i] = 0;
                        break;
                    }
                }
                temp[gunID] = 1;
            }
            //if the selected gun is index 1 switch out the second gun slot
            if (selectedGun == 1)
            {

                int[] temp = GetComponentInChildren<PlayerShoot>().bullet2;
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] != 0)
                    {
                        oldGunID = i;
                        temp[i] = 0;
                        break;
                    }
                }
                temp[gunID] = 1;
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
            Destroy(collision.gameObject);
        }
    }
}
