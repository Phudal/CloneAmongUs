using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    public static LobbyUIManager Instance;

    [SerializeField] private CustomizeUI _customizeUI;

    public CustomizeUI CustomizeUI
    {
        get { return _customizeUI; }
    }

    [SerializeField] private Button useButton;

    [SerializeField] private Sprite originUseButtionSprite;

    private void Awake()
    {
        Instance = this;
    }

    public void SetUseButton(Sprite sprite, UnityAction action)
    {
        useButton.image.sprite = sprite;
        useButton.onClick.AddListener(action);
        useButton.interactable = true;
    }
    
    public void UnsetUseButton()
    {
        useButton.image.sprite = originUseButtionSprite;
        useButton.onClick.RemoveAllListeners();
        useButton.interactable = false;
    }
}