using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(key: KeyCode.D))
        {
            _animator.SetBool("isRunningRight", true);
        }
        else
        {
            _animator.SetBool("isRunningRight", false);
        }



        if (Input.GetKey(key: KeyCode.A))
        {
            _animator.SetBool("isRunningLeft", true);
        } else
        {
            _animator.SetBool("isRunningLeft", false);
        }

        if (Input.GetKey(key: KeyCode.W) &&
            Input.GetKey(key: KeyCode.A) &&
            Input.GetKey(key: KeyCode.Mouse1))
                {
            _animator.SetBool("isShootingTopLeft", true);
        } else
        {
            _animator.SetBool("isShootingTopLeft", false);
        }
    }

}
