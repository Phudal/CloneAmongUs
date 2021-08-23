using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
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
    
    
    [SerializeField] private GameRoomPlayerCounter _gameRoomPlayerCounter;
    public GameRoomPlayerCounter GameRoomPlayerCounter
    {
        get { return _gameRoomPlayerCounter; }
    }

    
    [SerializeField] private Button useButton;

    [SerializeField] private Button startButton;
    
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

    public void ActiveStartButton()
    {
        startButton.gameObject.SetActive(true);
    }

    public void SetInteractableStartButton(bool isInteractable)
    {
        startButton.interactable = isInteractable;
    }

    public void OnClickStartButton()
    {
        var players = FindObjectsOfType<RoomPlayer>();

        for (int i = 0; i < players.Length; i++)
        {
            players[i].readyToBegin = true;
        }

        var manager = NetworkManager.singleton as RoomManager;
        manager.ServerChangeScene(manager.GameplayScene);
    }
}