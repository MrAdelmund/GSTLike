using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuControler : MonoBehaviour
{
    [SerializeField] Animator animatorForAnimation;
    static bool pauseMenuCanOpen = true;
    Canvas canvas;
    bool toggeledOn = false;
    static CanvasGroup canvasGroup;
    void Start()
    {
        //component retrieval
        canvas = GetComponent<Canvas>();
        canvasGroup = GetComponent<CanvasGroup>();
        canvas.enabled = false;
        animatorForAnimation.enabled = false;
        //makes canvas not interactable
        canvasGroup.interactable = false;
    }
    void Pause()
    {
        canvas.enabled = true;
        Time.timeScale = 0;
        //toggles on menu animation
        animatorForAnimation.enabled = true;
        //makes canvas interactable
        canvasGroup.interactable = true;
    }
    public void Resume()
    {
        canvas.enabled = false;
        Time.timeScale = 1;
        //toggles off menu animation
        animatorForAnimation.enabled = false;
        //resets bool to keep stuff synced
        toggeledOn = false;
        //makes canvas not interactable
        canvasGroup.interactable = false;
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void ReturnToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    //These are functions that can be called from anywhere without an object
    //reference, to disable/enable the player's ability to open the pause menu.
    public static void DisablePauseMenuOpening()
    {
        pauseMenuCanOpen = false;
    }
    public static void EnablePauseMenuOpening()
    {
        pauseMenuCanOpen = true;
    }
    public void TogglePauseMenu(InputAction.CallbackContext context)
    {
        //gets called when player presses pause button, and flips the current state of the menu
        if (pauseMenuCanOpen)
        {
            if (context.performed)
                toggeledOn = !toggeledOn;
            if (toggeledOn)
                Pause();
            else
                Resume();
        }
    }
}
