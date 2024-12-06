using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryManager : MonoBehaviour
{
    private Image[] _inventorySlots;

    private void Start()
    {
        _inventorySlots = GetComponentsInChildren<Image>();
    }
    
    
}
