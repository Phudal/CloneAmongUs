using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPipeLight : MonoBehaviour
{
    private Animator _animator;

    private WaitForSeconds wait = new WaitForSeconds(0.15f);

    private List<WeaponPipeLight> lights = new List<WeaponPipeLight>();
    private static readonly int On = Animator.StringToHash("On");

    private void Start()
    {
        _animator = GetComponent<Animator>();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i).GetComponent<WeaponPipeLight>();
            if (child)
            {
                lights.Add(child);
            }
        }
    }

    public void TurnOnLight()
    {
        _animator.SetTrigger(On);
        StartCoroutine(TurnOnLightAtChild());
    }

    private IEnumerator TurnOnLightAtChild()
    {
        yield return wait;
        
        foreach (var child in lights)
        {
            child.TurnOnLight();
        }
    }
}
