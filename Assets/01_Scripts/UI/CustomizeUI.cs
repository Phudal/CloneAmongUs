using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class CustomizeUI : MonoBehaviour
{
    [SerializeField] private Image characterPreview;

    [SerializeField] private List<ColorSelectButton> colorSelectButtons;
    
    // Start is called before the first frame update
    void Start()
    {
        var inst = Instantiate(characterPreview.material);
        characterPreview.material = inst;
    }

    private void OnEnable()
    {
        UpdateColorButton();

        var roomSlots = (NetworkManager.singleton as RoomManager).roomSlots;

        foreach (var player in roomSlots)
        {
            var aPlayer = player as RoomPlayer;
            if (aPlayer.isLocalPlayer)
            {
                UpdatePreviewColor(aPlayer.playerColor);
                break;
            }
        }
    }

    public void UpdateColorButton()
    {
        var roomSlots = (NetworkManager.singleton as RoomManager).roomSlots;

        for (int i = 0; i < colorSelectButtons.Count; i++)
        {
            colorSelectButtons[i].SetInteractable(true);
        }

        foreach (var player in roomSlots)
        {
            var aPlayer = player as RoomPlayer;
            colorSelectButtons[(int)aPlayer.playerColor].SetInteractable(false);
        }
    }

    public void UpdateSelectColorButton(EPlayerColor color)
    {
        colorSelectButtons[(int) color].SetInteractable(false);
    }
    
    public void UpdateUnselectColorButton(EPlayerColor color)
    {
        colorSelectButtons[(int) color].SetInteractable(true);
    }
    
    public void UpdatePreviewColor(EPlayerColor color)
    {
        characterPreview.material.SetColor("_PlayerColor", PlayerColor.GetColor(color));
    }

    public void OnClickColorButton(int index)
    {
        if (colorSelectButtons[index].bIsInteractable)
        {
            RoomPlayer.MyRoomPlayer.CmdSetPlayerColor((EPlayerColor) index);
            UpdatePreviewColor((EPlayerColor) index);
        }
    }

    public void Open()
    {
        RoomPlayer.MyRoomPlayer.lobbyPlayerCharacter.BIsMoveable = false;
        gameObject.SetActive(true);
    }
    
    public void Close()
    {
        RoomPlayer.MyRoomPlayer.lobbyPlayerCharacter.BIsMoveable = true;
        gameObject.SetActive(false);
    }
}
