using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameRoomPlayerCounter : NetworkBehaviour
{
    [SyncVar] private int minPlayer;
    [SyncVar] private int maxPlayer;
    
    [SerializeField] private Text playerCountText;

    void Start()
    {
        if (isServer)
        {
            var manager = NetworkManager.singleton as RoomManager;
            minPlayer = manager.minPlayerCount;
            maxPlayer = manager.maxConnections;
        }
    }
    
    public void UpdatePlayerCount()
    {
        var players = FindObjectsOfType<RoomPlayer>();
        bool bIsStartable = players.Length >= minPlayer;

        if (bIsStartable)
            playerCountText.color = Color.white;

        else
            playerCountText.color = Color.red;
        
        playerCountText.text = string.Format("{0}/{1}", players.Length, maxPlayer);
        
        LobbyUIManager.Instance.SetInteractableStartButton(bIsStartable);
    }
}
