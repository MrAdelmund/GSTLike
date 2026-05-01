using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraControler : MonoBehaviour
{
    [SerializeField] GameObject objectToFollow;
    [SerializeField] float ZPosition = -10;
    [SerializeField] float horizontalMaxDistance = 3;
    [SerializeField] bool cameraCanMoveLeft = true;
    [SerializeField] bool cameraCanMoveRight = true;
    Transform objectFollowTrans;
    Vector3 objectFollowLastPos;
    private void Start()
    {
        objectFollowTrans = objectToFollow.GetComponent<Transform>();
        objectFollowLastPos = objectFollowTrans.position;
    }
    private void Update()
    {
        if (objectFollowTrans.position != objectFollowLastPos)
        {
            objectFollowLastPos = objectFollowTrans.position;
            UpdateCameraPosition();
        }
    }
    void UpdateCameraPosition()
    {
        float horzDistnace = objectFollowTrans.position.x - transform.position.x;
        float XPosToUse = 0;
        if (horzDistnace > horizontalMaxDistance && cameraCanMoveRight)
        {
            XPosToUse = transform.position.x + (horzDistnace - horizontalMaxDistance);
        } else if (horzDistnace < -1 * horizontalMaxDistance && cameraCanMoveLeft)
        {
            XPosToUse = transform.position.x + (horzDistnace + horizontalMaxDistance);
        }
        if (XPosToUse == 0)
            XPosToUse = transform.position.x;
        transform.position = new Vector3(XPosToUse, transform.position.y, ZPosition);
    }
}
