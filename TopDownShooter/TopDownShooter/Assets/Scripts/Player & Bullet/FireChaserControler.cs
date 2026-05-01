using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireChaserControler : MonoBehaviour
{
    [SerializeField] GameObject[] objectChain;
    [SerializeField] int chainDistance = 2;
    [SerializeField] List<Vector2> positionList;
    [SerializeField] List<float> ZRotationList;
    bool doneSpawning = false;
    float bulletSpeed;
    Rigidbody2D headRB;
    [HideInInspector] public Vector2 aimInput;
    [HideInInspector] public bool activePlayerControl = true;
    [HideInInspector] public float playerFacingDir;
    void Start()
    {
        //get the rigidbody of the head of the chain
        headRB = objectChain[0].GetComponent<Rigidbody2D>();
        bulletSpeed = GetComponent<BulletData>().bulletSpeed;
        //sets initial velocity for projectile to follow
        if (aimInput == Vector2.zero)
            aimInput = new Vector2(playerFacingDir, 0);
        headRB.velocity = aimInput.normalized * bulletSpeed;
    }
    void FixedUpdate()
    {
        //sets rotation and velocity of the head object based off player input
        if (aimInput != Vector2.zero)
        {
            objectChain[0].transform.up = (aimInput.normalized);
            headRB.velocity = objectChain[0].transform.up * bulletSpeed;
        }
        //calls functions to update object chain positions & rotations
        if (doneSpawning)
        {
            UpdateObjectChainPositon();
        }
        else
        {
            UpdateObjectChainPositonSpawning();
        }
    }
    //only runs when the projectile is still spawning in
    //a larger version of UpdateObjectChainPositon() that will also enable new segments when applicable 
    void UpdateObjectChainPositonSpawning()
    {
        //updates lists for position and rotation of segments
        positionList.Insert(0, objectChain[0].transform.position);
        ZRotationList.Insert(0, objectChain[0].transform.eulerAngles.z);
        //If player control over the object is relinquished, this will stop new segments of the chain from being enabled.
        if (!activePlayerControl)
        {
            positionList.RemoveAt(positionList.Count - 1);
            ZRotationList.RemoveAt(ZRotationList.Count - 1);
        }
        //checks the entire projectile is spawned in
        if (positionList.Count > chainDistance * (objectChain.Length))
        {
            doneSpawning = true;
        }
        
        for (int i = 1; i < positionList.Count / chainDistance; i++)
        {
            if (!(objectChain[i].activeSelf) && i + 1 < objectChain.Length)
            {
                objectChain[i].SetActive(true);
            }
            objectChain[i].transform.position = positionList[i * chainDistance];
            objectChain[i].transform.eulerAngles = new Vector3(0, 0, ZRotationList[i * chainDistance]);
        }
    }
    //Runs when the whole chain is spawned in. All this does differently is it has less
    //checks because the whole projectile chain is already spawned in.
    void UpdateObjectChainPositon()
    {
        //updates lists for position and rotation of segments
        positionList.Insert(0, objectChain[0].transform.position);
        positionList.RemoveAt(positionList.Count - 1);
        ZRotationList.Insert(0, objectChain[0].transform.eulerAngles.z);
        ZRotationList.RemoveAt(ZRotationList.Count - 1);
        //applies position and rotation from lists to object chain
        for (int i = 1; i < objectChain.Length; i++)
        {
            objectChain[i].transform.position = positionList[i * chainDistance];
            objectChain[i].transform.eulerAngles = new Vector3(0, 0, ZRotationList[i * chainDistance]);
        }
    }
}