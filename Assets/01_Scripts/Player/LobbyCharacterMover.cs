using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class LobbyCharacterMover : CharacterMover
{
    [SyncVar(hook = nameof(SetOwnerNetId_Hook))] 
    public uint ownerNetId;

    public void CompleteSpawn()
    {
        if (hasAuthority)
        {
            BIsMoveable = true;
        }
    }
    
    public void SetOwnerNetId_Hook(uint _, uint newOwnerId)
    {
        var players = FindObjectsOfType<RoomPlayer>();
        foreach (var player in players)
        {
            if (newOwnerId == player.netId)
            {
                player.lobbyPlayerCharacter = this;
                break;
            }
        }
    }
    
}
