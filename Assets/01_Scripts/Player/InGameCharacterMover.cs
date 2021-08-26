using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class InGameCharacterMover : CharacterMover
{
    protected override void Start()
    {
        base.Start();

        if (hasAuthority)
        {
            BIsMoveable = true;
        
            var myRoomPlayer = RoomPlayer.MyRoomPlayer;

            CmdSetPlayerCharacter(myRoomPlayer.nickName, myRoomPlayer.playerColor);
        }
    }

    [Command]
    private void CmdSetPlayerCharacter(string nickname, EPlayerColor color)
    {
        this.nickName = nickname;
        playerColor = color;
    }
}
