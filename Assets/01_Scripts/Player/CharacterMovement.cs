using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CharacterMovement : NetworkBehaviour
{
    private Animator _animator;
    
    public bool bIsMoveable;

    private bool bIsMove = false;
    
    [SyncVar] public float speed = 2.0f;
    
    private static readonly int IsMove = Animator.StringToHash("IsMove");

    // Start is called before the first frame update
    private void Start()
    {
        _animator = GetComponent<Animator>();
        
        if (hasAuthority)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0.0f, 0.0f, -10.0f);
            cam.orthographicSize = 2.5f;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if (hasAuthority && bIsMoveable)
        {
            bIsMove = false;
            if (PlayerSettings.GetControlMode() == EControlType.KeyboardMouse)
            {
                Vector3 dir = Vector3.ClampMagnitude(
                    new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f), 1.0f);
                if (dir.x < 0.0f)
                    transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
                
                else if (dir.x > 0.0f)
                    transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

                transform.position += dir * speed * Time.deltaTime;

                bIsMove = dir.magnitude != 0.0f;
            }

            else
            {
                if (Input.GetMouseButton(0))
                {
                    Vector3 dir = (Input.mousePosition -
                                   new Vector3(Screen.width * 0.5f, Screen.height * 0.5f, 0.0f)).normalized;
                    
                    if (dir.x < 0.0f)
                        transform.localScale = new Vector3(-0.5f, 0.5f, 1.0f);
                
                    else if (dir.x > 0.0f)
                        transform.localScale = new Vector3(0.5f, 0.5f, 1.0f);

                    transform.position += dir * speed * Time.deltaTime;
                    
                    bIsMove = dir.magnitude != 0.0f;
                }
            }
            
            _animator.SetBool(IsMove, bIsMove);
        }
    }
}
