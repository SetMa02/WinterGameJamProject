using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryManager : MonoBehaviour
{
    public Image[] _inventorySlots;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private Dictionary<Image, string> _itemsImages;

    private void Start()
    {
        // Подписка на событие
        if (_inventoryManager != null)
        {
            _inventoryManager.OnInventoryChanged += UpdateImages;
            Debug.Log("Subscribed to OnInventoryChanged");
        }
        else
        {
            Debug.LogError("InventoryManager is not assigned!");
        }
        
        //_inventorySlots = GetComponentsInChildren<Image>();
    }

    private void OnDestroy()
    {
        if (_inventoryManager != null)
        {
            _inventoryManager.OnInventoryChanged -= UpdateImages;
        }
    }

    private void UpdateImages()
    {
        Debug.Log("Inventory updated");
        foreach (var item in _inventoryManager._slots)
        {
            if (item != null)
            {
                int index = Array.IndexOf(_inventoryManager._slots, item);
                if (index >= 0 && index < _inventorySlots.Length)
                {
                    _inventorySlots[index].sprite = item.icon;
                    Debug.Log("Updated slot " + index);
                }
            }
        }
    }
}
