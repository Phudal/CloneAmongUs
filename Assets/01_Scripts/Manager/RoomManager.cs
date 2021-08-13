using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RoomManager : NetworkRoomManager
{
    // 서버에서 새로 접속한 클라이언트를 감지하였을 때 실행
    public override void OnRoomServerConnect(NetworkConnection conn)
    {
        base.OnRoomServerConnect(conn);

        // 스폰 포지션을 가져옴
        Vector3 spawnPos = FindObjectOfType<SpawnPosition>().GetSpawnPosition();
        
        // 플레이어를 생성 후
        var player = Instantiate(spawnPrefabs[0], spawnPos, Quaternion.identity);
        // 방금 생성된 오브젝트가 새로 접속한 플레이어 소유임을 알림  
        NetworkServer.Spawn(player, conn);
    }
}
