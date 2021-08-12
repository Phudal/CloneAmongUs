using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class CreateRoomUI : MonoBehaviour
{
    [SerializeField] private List<Image> crewImgs;

    [SerializeField] private List<Button> imposterCountButtons;

    [SerializeField] private List<Button> maxPlayerCountButtons;

    private CreateGameRoomData _roomData;

    private void Start()
    {
        for (int i = 0; i < crewImgs.Count; i++)
        {
            Material materialInstance = Instantiate(crewImgs[i].material);
            crewImgs[i].material = materialInstance;
        }
        
        _roomData = new CreateGameRoomData()
        {
            ImposterCount = 1,
            MaxPlayerCount = 10
        };
        
        UpdateCrewImages();
    }

    public void UpdateImposterCount(int count)
    {
        _roomData.ImposterCount = count;
        
        for (int i = 0; i < imposterCountButtons.Count; i++)
        {
            if (i == count - 1)
            {
                imposterCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                imposterCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }

        int limitMaxPlayer = count == 1 ? 4 : count == 2 ? 7 : 9;

        if (_roomData.MaxPlayerCount < limitMaxPlayer)
        {
            UpdateMaxPlayerCount(limitMaxPlayer);
        }
        else
        {
            UpdateMaxPlayerCount(_roomData.MaxPlayerCount);
        }

        for (int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            var text = maxPlayerCountButtons[i].GetComponentInChildren<Text>();

            if (i < limitMaxPlayer - 4)
            {
                maxPlayerCountButtons[i].interactable = false;
                text.color = Color.grey;
            }
            else
            {
                maxPlayerCountButtons[i].interactable = true;
                text.color = Color.white;
            }
        }
            
    }
    
    public void UpdateMaxPlayerCount(int count)
    {
        _roomData.MaxPlayerCount = count;

        for (int i = 0; i < maxPlayerCountButtons.Count; i++)
        {
            if (i == count - 4)
            {
                maxPlayerCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            }
            else
            {
                maxPlayerCountButtons[i].image.color = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            }
        }
        
        UpdateCrewImages();
    }
    
    private void UpdateCrewImages()
    {
        for (int i = 0; i < crewImgs.Count; i++)
        {
            crewImgs[i].material.SetColor("_PlayerColor", Color.white);
        }

        int imposterCount = _roomData.ImposterCount;
        int idx = 0;

        while (imposterCount != 0)
        {
            if (idx >= _roomData.MaxPlayerCount)
            {
                idx = 0;
            }

            if (crewImgs[idx].material.GetColor("_PlayerColor") != Color.red && Random.Range(0, 5) == 0)
            {
                crewImgs[idx].material.SetColor("_PlayerColor", Color.red);
                imposterCount--;
            }

            idx++;
        }

        for (int i = 0; i < crewImgs.Count; i++)
        {
            if (i < _roomData.MaxPlayerCount)
            {
                crewImgs[i].gameObject.SetActive(true);
            }
            else
            {
                crewImgs[i].gameObject.SetActive(false);
            }
        }
    }
}

