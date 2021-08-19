using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelectButton : MonoBehaviour
{
    [SerializeField] private GameObject x;

    public bool bIsInteractable = true;

    public void SetInteractable(bool isInteractale)
    {
        this.bIsInteractable = isInteractale;
        x.SetActive(!isInteractale);
    }
}
