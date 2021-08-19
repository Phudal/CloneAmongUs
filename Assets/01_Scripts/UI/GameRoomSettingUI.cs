using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoomSettingUI : SettingUI
{
    public void Open()
    {
        RoomPlayer.MyRoomPlayer.lobbyPlayerCharacter.BIsMoveable = false;
        gameObject.SetActive(true);
    }
    
    public override void Close()
    {
        base.Close();
        RoomPlayer.MyRoomPlayer.lobbyPlayerCharacter.BIsMoveable = true;
    }
    
    public void ExitGameRoom()
    {
        var manager = RoomManager.singleton;

        // 호스트일 때
        if (manager.mode == Mirror.NetworkManagerMode.Host)
        {
            manager.StopHost();
        }
        
        // 클라이언트일 때
        else if (manager.mode == Mirror.NetworkManagerMode.ClientOnly)
        {
            manager.StopClient();
        }
    }
}
