using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCrew : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private EPlayerColor _playerColor;
    
    private Vector3 _direction;

    private float _floatingSpeed;

    private float _rotateSpeed;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        transform.position += _direction * _floatingSpeed * Time.deltaTime;
        transform.rotation = 
            Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0.0f, 0.0f, _rotateSpeed));
    }

    public void SetFloatingCrew(
        Sprite sprite,
        EPlayerColor playerColor,
        Vector3 direction,
        float floatingSpeed,
        float rotateSpeed,
        float size)
    {
        this._playerColor = playerColor;
        this._direction = direction;
        this._floatingSpeed = floatingSpeed;
        this._rotateSpeed = rotateSpeed;

        _spriteRenderer.sprite = sprite;
        _spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));

        transform.localScale = new Vector3(size, size, size);
        _spriteRenderer.sortingOrder = (int) Mathf.Lerp(1, 32767, size);
    }
}
