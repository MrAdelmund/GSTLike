using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CopySprite : MonoBehaviour
{
    public Image image;
    void Update()
    {
        GetComponent<Image>().sprite = image.sprite;
    }
}
