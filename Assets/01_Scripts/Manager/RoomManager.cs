using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoomManager : NetworkRoomManager
{
    public int minPlayerCount;
    public int imposterCount;
    
    // 서버에서 새로 접속한 클라이언트를 감지하였을 때 실행
    public override void OnRoomServerConnect(NetworkConnection conn)
    {
        base.OnRoomServerConnect(conn);
        
    }
}
