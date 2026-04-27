using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator _animator;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key: KeyCode.D))
        {
            _animator.SetBool("isRunningRight", true);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isShootingTopLeft", false);
            _animator.SetBool("isShootingLeft", false);
            _animator.SetBool("isShootingBotttomLeft", false);
            _animator.SetBool("isShootingDown", false);
            _animator.SetBool("isShootingBottomRight", false);
            _animator.SetBool("isShootingRight", false);
            _animator.SetBool("isShootingTopRight", false);
            _animator.SetBool("isShootingUp", false);
        }
        else
        {
            _animator.SetBool("isRunningRight", false);
        }



        if (Input.GetKey(key: KeyCode.A))
        {
            _animator.SetBool("isRunningLeft", true);
            _animator.SetBool("isRunningRight", false);
            _animator.SetBool("isShootingTopLeft", false);
            _animator.SetBool("isShootingLeft", false);
            _animator.SetBool("isShootingBotttomLeft", false);
            _animator.SetBool("isShootingDown", false);
            _animator.SetBool("isShootingBottomRight", false);
            _animator.SetBool("isShootingRight", false);
            _animator.SetBool("isShootingTopRight", false);
            _animator.SetBool("isShootingUp", false);
        }
        else {
            _animator.SetBool("isRunningLeft", false);
        }

        if (Input.GetKey(key: KeyCode.W) &&
            Input.GetKey(key: KeyCode.A) &&
            Input.GetKey(key: KeyCode.Mouse0))
                {
            _animator.SetBool("isShootingTopLeft", true);
            _animator.SetBool("isRunningRight", false);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isShootingLeft", false);
            _animator.SetBool("isShootingBotttomLeft", false);
            _animator.SetBool("isShootingDown", false);
            _animator.SetBool("isShootingBottomRight", false);
            _animator.SetBool("isShootingRight", false);
            _animator.SetBool("isShootingTopRight", false);
            _animator.SetBool("isShootingUp", false);
        } else {
            _animator.SetBool("isShootingTopLeft", false);
        }

        if (Input.GetKey(key: KeyCode.A) &&
            Input.GetKey(key: KeyCode.Mouse0))
        {
            _animator.SetBool("isShootingLeft", true);
            _animator.SetBool("isRunningRight", false);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isShootingTopLeft", false);
            _animator.SetBool("isShootingBotttomLeft", false);
            _animator.SetBool("isShootingDown", false);
            _animator.SetBool("isShootingBottomRight", false);
            _animator.SetBool("isShootingRight", false);
            _animator.SetBool("isShootingTopRight", false);
            _animator.SetBool("isShootingUp", false);
        } else {
        _animator.SetBool("isShootingLeft", false);
                }
        if (Input.GetKey(key: KeyCode.A) &&
            Input.GetKey(key:KeyCode.S) &&
            Input.GetKey(key:KeyCode.Mouse0))
        {
            _animator.SetBool("isShootingBottomLeft", true);
            _animator.SetBool("isRunningRight", false);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isShootingTopLeft", false);
            _animator.SetBool("isShootingLeft", false);
            _animator.SetBool("isShootingDown", false);
            _animator.SetBool("isShootingBottomRight", false);
            _animator.SetBool("isShootingRight", false);
            _animator.SetBool("isShootingTopRight", false);
            _animator.SetBool("isShootingUp", false);
        } else {
            _animator.SetBool("isShootingBotttomLeft", false);
        }
        if (Input.GetKey(key:KeyCode.S) &&
            Input.GetKey(key:KeyCode.Mouse0))
        {
            _animator.SetBool("isShootingDown", true);
            _animator.SetBool("isRunningRight", false);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isShootingTopLeft", false);
            _animator.SetBool("isShootingLeft", false);
            _animator.SetBool("isShootingBotttomLeft", false);
            _animator.SetBool("isShootingBottomRight", false);
            _animator.SetBool("isShootingRight", false);
            _animator.SetBool("isShootingTopRight", false);
            _animator.SetBool("isShootingUp", false);
        } else {
            _animator.SetBool("isShootingDown", false);
        }
        if (Input.GetKey(key: KeyCode.S) &&
            Input.GetKey(key: KeyCode.D) &&
            Input.GetKey(key: KeyCode.Mouse0))
        {
            _animator.SetBool("isShootingBottomRight", true);
            _animator.SetBool("isRunningRight", false);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isShootingTopLeft", false);
            _animator.SetBool("isShootingLeft", false);
            _animator.SetBool("isShootingBotttomLeft", false);
            _animator.SetBool("isShootingDown", false);
            _animator.SetBool("isShootingRight", false);
            _animator.SetBool("isShootingTopRight", false);
            _animator.SetBool("isShootingUp", false);
        } else {
            _animator.SetBool("isShootingBottomRight", false);
        }
        if (Input.GetKey(key: KeyCode.D) &&
            Input.GetKey(key: KeyCode.Mouse0))
        {
            _animator.SetBool("isShootingRight", true);
            _animator.SetBool("isRunningRight", false);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isShootingTopLeft", false);
            _animator.SetBool("isShootingLeft", false);
            _animator.SetBool("isShootingBotttomLeft", false);
            _animator.SetBool("isShootingDown", false);
            _animator.SetBool("isShootingBottomRight", false);
            _animator.SetBool("isShootingTopRight", false);
            _animator.SetBool("isShootingUp", false);
        } else {
            _animator.SetBool("isShootingRight", false);
        }
        if (Input.GetKey(key: KeyCode.W) &&
            Input.GetKey(key: KeyCode.D) &&
            Input.GetKey(key: KeyCode.Mouse0))
        {
            _animator.SetBool("isShootingTopRight", true);
            _animator.SetBool("isRunningRight", false);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isShootingTopLeft", false);
            _animator.SetBool("isShootingLeft", false);
            _animator.SetBool("isShootingBotttomLeft", false);
            _animator.SetBool("isShootingDown", false);
            _animator.SetBool("isShootingBottomRight", false);
            _animator.SetBool("isShootingRight", false);
            _animator.SetBool("isShootingUp", false);
        } else {
            _animator.SetBool("isShootingTopRight", false);
        }
        if (Input.GetKey(key: KeyCode.W) &&
            Input.GetKey(key: KeyCode.Mouse0))
        {
            _animator.SetBool("isShootingUp", true);
            _animator.SetBool("isRunningRight", false);
            _animator.SetBool("isRunningLeft", false);
            _animator.SetBool("isShootingTopLeft", false);
            _animator.SetBool("isShootingLeft", false);
            _animator.SetBool("isShootingBotttomLeft", false);
            _animator.SetBool("isShootingDown", false);
            _animator.SetBool("isShootingBottomRight", false);
            _animator.SetBool("isShootingRight", false);
            _animator.SetBool("isShootingTopRight", false);
        }
        else
        {
            _animator.SetBool("isShootingUp", false);
        }

    }

}
