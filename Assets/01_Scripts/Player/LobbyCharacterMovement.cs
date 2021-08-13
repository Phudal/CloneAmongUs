using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCharacterMovement : CharacterMovement
{
    public void CompleteSpawn()
    {
        if (hasAuthority)
        {
            bIsMoveable = true;
        }
    }
}
