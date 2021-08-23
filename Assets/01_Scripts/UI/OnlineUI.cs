using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class OnlineUI : MonoBehaviour
{
    [SerializeField] private InputField nicknameInputField;

    [SerializeField] private GameObject createRoomUI;
    private static readonly int On = Animator.StringToHash("On");

    public void OnClickCreateRoomButton()
    {
        if (nicknameInputField.text != "")
        {
            PlayerSettings.nickname = nicknameInputField.text;
            createRoomUI.SetActive(true);
            gameObject.SetActive(false);
        }

        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger(On);
        }
    }

    public void OnClickEnterGameRoomButton()
    {
        if (nicknameInputField.text != "")
        {
            PlayerSettings.nickname = nicknameInputField.text;
            var manager = RoomManager.singleton;
            manager.StartClient();
        }

        else
        {
            nicknameInputField.GetComponent<Animator>().SetTrigger(On);
        }
        
        
    }
}
