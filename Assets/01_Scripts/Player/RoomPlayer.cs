using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Rendering;

public class RoomPlayer : NetworkRoomPlayer
{
    private static RoomPlayer _myRoomPlayer;
    public static RoomPlayer MyRoomPlayer
    {
        get
        {
            if (_myRoomPlayer == null)
            {
                var players = FindObjectsOfType<RoomPlayer>();
                foreach (var player in players)
                {
                    if (player.hasAuthority)
                    {
                        _myRoomPlayer = player;
                    }
                }
            }

            return _myRoomPlayer;
        }
    }

    [SyncVar(hook = nameof(SetPlayerColor_Hook))] 
    public EPlayerColor playerColor;

    public CharacterMover lobbyPlayerCharacter;
    
    private void Start()
    {
        base.Start();
        
        // 서버일 때만 호출
        if (isServer)
            SpawnLobbyPlayerCharacter();
    }

    private void OnDestroy()
    {
        if (LobbyUIManager.Instance != null)
            LobbyUIManager.Instance.CustomizeUI.UpdateUnselectColorButton(playerColor);
    }

    // Mirror API에서 제공
    // 클라이언트에서 함수 호출 시
    // 함수 내부 동작이 서버 내에서 돌아가도록 함
    // 함수 앞에 접두사로 Cmd를 반드시 붙여야함
    [Command]
    public void CmdSetPlayerColor(EPlayerColor color)
    {
        playerColor = color;
        lobbyPlayerCharacter.playerColor = color;
    }
    
    private void SpawnLobbyPlayerCharacter()
    {
        // 대기실에 대기중인 플레이어들을 가져옴
        var roomSlots = (NetworkManager.singleton as RoomManager).roomSlots;
        EPlayerColor color = EPlayerColor.Red;

        // roomSlots를 순회하면서 플레이어들이 사용하지 않은 색상을 고름
        for (int i = 0; i < (int) EPlayerColor.Lime + 1; i++)
        {
            bool bIsFindSameColor = false;
            foreach (var roomPlayer in roomSlots)
            {
                var amongusRoomManager = roomPlayer as RoomPlayer;
                if (amongusRoomManager.playerColor == (EPlayerColor) i && roomPlayer.netId != netId)
                {
                    bIsFindSameColor = true;
                    break;
                }
            }
            
            // 찾지 못했다면
            if (!bIsFindSameColor)
            {
                color = (EPlayerColor) i;
                break;
            }
        }

        playerColor = color;
        
        
        // 스폰 포지션을 가져옴
        Vector3 spawnPos = FindObjectOfType<SpawnPosition>().GetSpawnPosition();
        
        // 플레이어를 생성 후
        var playerCharacter = Instantiate(RoomManager.singleton.spawnPrefabs[0], spawnPos, Quaternion.identity).
            GetComponent<LobbyCharacterMover>();

        playerCharacter.transform.localScale =
            index < 5 ? new Vector3(0.5f, 0.5f, 1.0f) : new Vector3(-0.5f, 0.5f, 1.0f);
        
        // 방금 생성된 오브젝트가 새로 접속한 플레이어 소유임을 알림  
        NetworkServer.Spawn(playerCharacter.gameObject, connectionToClient);
        playerCharacter.ownerNetId = netId;
        playerCharacter.playerColor = color;
    }


    public void SetPlayerColor_Hook(EPlayerColor oldColor, EPlayerColor newColor)
    {
        LobbyUIManager.Instance.CustomizeUI.UpdateSelectColorButton(newColor);
    }
}
