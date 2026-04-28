using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation2 : MonoBehaviour
{
    Animator anim;
    SpriteRenderer spr;
    //jump not currently used, but here for future development.
    bool jumpPressed = false;
    Vector2 aimInput;
    int x;
    int y;
    private void Start()
    {
        anim = GetComponent<Animator>();
        spr = GetComponent<SpriteRenderer>();
    }
    void ProcessPlayerAimInput()
    {
        //rounds player inputs to ints
        x = Mathf.RoundToInt(aimInput.x);
        y = Mathf.RoundToInt(aimInput.y);

        //sets values for animator to use
        anim.SetInteger("PlayerXInput", x);
        anim.SetInteger("PlayerYInput", y);

        //updates the flip x of the player's sprite render
        if (x > 0)
            spr.flipX = false;
        else if (x < 0)
            spr.flipX = true;
    }
    //input updaters
    public void PlayerInputJump(InputAction.CallbackContext context)
    {
        jumpPressed = context.performed;
    }
    public void PlayerInputAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
        ProcessPlayerAimInput();
    }
    public void PlayerInputFire(InputAction.CallbackContext context)
    {
        anim.SetBool("FirePressed", context.performed);
    }
}
