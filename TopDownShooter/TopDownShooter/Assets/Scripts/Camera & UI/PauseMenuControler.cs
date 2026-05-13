using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuControler : MonoBehaviour
{
    [SerializeField] Animator animatorForAnimation;
    static bool pauseMenuCanOpen = true;
    Canvas canvas;
    bool toggeledOn = false;

    void Start()
    {
        canvas = GetComponent<Canvas>();
        canvas.enabled = false;
        animatorForAnimation.enabled = false;
    }
    public void TogglePauseMenu(InputAction.CallbackContext context)
    {
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
    void Pause()
    {
        canvas.enabled = true;
        Time.timeScale = 0;
        animatorForAnimation.enabled = true;
    }
    public void Resume()
    {
        canvas.enabled = false;
        Time.timeScale = 1;
        animatorForAnimation.enabled = false;
    }
    public void Restart()
    {

    }
    public void ReturnToMenu()
    {

    }
    public static void DisablePauseMenuOpening()
    {
        pauseMenuCanOpen = false;
    }
    public static void EnablePauseMenuOpening()
    {
        pauseMenuCanOpen = false;
    }
}
