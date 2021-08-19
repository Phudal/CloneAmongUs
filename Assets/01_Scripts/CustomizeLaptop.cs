using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomizeLaptop : MonoBehaviour
{
    [SerializeField] private Sprite useButtonSprite;

    private SpriteRenderer _spriteRenderer;
    private static readonly int Highlighted = Shader.PropertyToID("_Highlighted");

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        var inst = Instantiate(_spriteRenderer.material);
        _spriteRenderer.material = inst;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var character = other.GetComponent<CharacterMover>();
        if (character != null && character.hasAuthority)
        {
            _spriteRenderer.material.SetFloat(Highlighted, 1.0f);
            LobbyUIManager.Instance.SetUseButton(useButtonSprite, OnClickUse);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var character = other.GetComponent<CharacterMover>();
        if (character != null && character.hasAuthority)
        {
            _spriteRenderer.material.SetFloat(Highlighted, 0.0f);
            LobbyUIManager.Instance.UnsetUseButton();
        }
    }

    public void OnClickUse()
    {
        LobbyUIManager.Instance.CustomizeUI.Open();
    }
}
