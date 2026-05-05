using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int startingVatality = 100;
    [SerializeField] float iFrameTimeGrantedWhenHit = 0.5f;
    int currentLives = 0;
    float iFrames = 0;
    void Start()
    {
        //retrives Vitality from PlayerPrefs
        currentLives = PlayerPrefs.GetInt("Vitality");
        //sets vitality when starting new game
        if (currentLives == 0)
        {
            PlayerPrefs.SetInt("Vitality", startingVatality);
            currentLives = startingVatality;
        }
    }
    void Update()
    {
        iFrames -= Time.deltaTime;
    }
    //gets called by enemy hitbox/enemy bullet object
    public void TakeDammage(int amountOfDammage)
    {
        //dammages player if they do not have Iframes
        if (iFrames <= 0)
        {
            currentLives -= amountOfDammage;
            iFrames = iFrameTimeGrantedWhenHit;
        }
        Debug.Log(currentLives);
    }
    //gets called by health containers when picked up
    public void GainHealth(int amountOfHealthToGain)
    {
        currentLives += amountOfHealthToGain;
    }
    //gets called by menu when level is complete
    public void UpdateSavedPlayerHealth()
    {
        PlayerPrefs.SetInt("Vitality", currentLives);
    }
}
