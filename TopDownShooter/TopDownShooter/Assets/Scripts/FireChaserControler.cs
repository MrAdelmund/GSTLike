using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FireChaserControler : MonoBehaviour
{
    [SerializeField] float moveSpeed = 15f;
    [SerializeField] Vector2[] PositionChain;
    [HideInInspector] public PlayerShoot shootScriptReference;
    Rigidbody2D rb;
    Vector2 aimInput;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        rb.velocity = (aimInput * moveSpeed);
    }
    public void PlayerInputAim(InputAction.CallbackContext context)
    {
        aimInput = context.ReadValue<Vector2>();
    }
}
