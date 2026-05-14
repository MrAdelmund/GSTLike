using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelClearTriggerer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            LevelClearControler.TriggerLevelClearScrene();
    }
}
