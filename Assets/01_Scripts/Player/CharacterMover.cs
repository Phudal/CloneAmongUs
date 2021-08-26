using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class CharacterMover : NetworkBehaviour
{
    private Animator _animator;
    
    private SpriteRenderer _spriteRenderer;

    [SerializeField] private Text nickNameText;
    
    private bool _bIsMoveable;

    public bool BIsMoveable
    {
        get
        {
            return _bIsMoveable;
        }
        set
        {
            if (!value)
            {
                _animator.SetBool(IsMove, false);
            }
            _bIsMoveable = value;
        }
    }

    private bool bIsMove = false;

    private static readonly int IsMove = Animator.StringToHash("IsMove");

    [SerializeField] private float characterSize = 0.5f;

    [SerializeField] private float cameraSize = 2.5f;
    
    // ---------------------------------------------- SyncVar --------------------------------------------------

    [SyncVar] public float speed = 2.0f;
    
    [SyncVar(hook = nameof(SetNicknameText_Hook))] 
    public string nickName;
    
    // 다른 클라이언트들과 동기화됨
    [SyncVar(hook = nameof(SetPlayerColor_Hook))]
    public EPlayerColor playerColor;
    
    
    // --------------------------------------------------------------------------------------------------------
    // ---------------------------------------------- Method --------------------------------------------------
    // --------------------------------------------------------------------------------------------------------
    
    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));
        
        _animator = GetComponent<Animator>();
        
        if (hasAuthority)
        {
            Camera cam = Camera.main;
            cam.transform.SetParent(transform);
            cam.transform.localPosition = new Vector3(0.0f, 0.0f, -10.0f);
            cam.orthographicSize = cameraSize;
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    // ---------------------------------------------- Custom Method ---------------------------------------------
    
    public void Move()
    {
        if (hasAuthority && BIsMoveable)
        {
            bIsMove = false;
            if (PlayerSettings.GetControlMode() == EControlType.KeyboardMouse)
            {
                Vector3 dir = Vector3.ClampMagnitude(
                    new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f), 1.0f);
                if (dir.x < 0.0f)
                    transform.localScale = new Vector3(-characterSize, characterSize, 1.0f);
                
                else if (dir.x > 0.0f)
                    transform.localScale = new Vector3(characterSize, characterSize, 1.0f);

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
                        transform.localScale = new Vector3(-characterSize, characterSize, 1.0f);
                
                    else if (dir.x > 0.0f)
                        transform.localScale = new Vector3(characterSize, characterSize, 1.0f);

                    transform.position += dir * speed * Time.deltaTime;
                    
                    bIsMove = dir.magnitude != 0.0f;
                }
            }
            
            _animator.SetBool(IsMove, bIsMove);
        }

        if (transform.localScale.x < 0)
            nickNameText.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        
        else if (transform.localScale.x > 0)
            nickNameText.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    // ---------------------------------------------- Hook ---------------------------------------------
    
    public void SetPlayerColor_Hook(EPlayerColor oldColor, EPlayerColor newColor)
    {
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        _spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(newColor));
    }

    public void SetNicknameText_Hook(string _, string value)
    {
        nickNameText.text = value;
    }
}
