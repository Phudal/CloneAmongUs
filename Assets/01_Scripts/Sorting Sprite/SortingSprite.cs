﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SortingSprite : MonoBehaviour
{
    [SerializeField] private ESortingType _sortingType;

    private SpriteSorter _sorter;

    private SpriteRenderer _spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        _sorter = FindObjectOfType<SpriteSorter>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _spriteRenderer.sortingOrder = _sorter.GetSortingOrder(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (_sortingType == ESortingType.Update)
        {
            _spriteRenderer.sortingOrder = _sorter.GetSortingOrder(gameObject);
        }
    }
}
