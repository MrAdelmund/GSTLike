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
    // Start is called before the first frame update
    void Start()
    {
        gun1.enabled = true;
        gun2.enabled = false;
    }

    // Update is called once per frame
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
        //Debug.Log(collision.gameObject.name);
        float y = Input.GetAxisRaw("Vertical");
        int oldGunID = -1;
        if (y < 0 && Input.GetButtonDown("SwapGun") && collision.gameObject.GetComponent<GunPickup>() != null)
        {
            
            int gunID = collision.gameObject.GetComponent<GunPickup>().gunIndex;
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
