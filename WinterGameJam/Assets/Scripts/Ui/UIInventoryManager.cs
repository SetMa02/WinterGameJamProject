using System;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryManager : MonoBehaviour
{
    [SerializeField] private Image[] _inventorySlots;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private Image _selector;

    private void Start()
    {
        if (_inventoryManager == null)
        {
            Debug.LogError("InventoryManager is not assigned!");
            return;
        }

        _inventoryManager.OnInventoryChanged += UpdateImages;
        Debug.Log("Subscribed to OnInventoryChanged");

        // Инициализация ячеек инвентаря при старте
        UpdateImages();
    }

    private void Update()
    {
        HandleSelectorMovement();
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
        Debug.Log("Updating inventory UI");

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (i < _inventoryManager._slots.Length && _inventoryManager._slots[i] != null)
            {
                _inventorySlots[i].sprite = _inventoryManager._slots[i].icon;
                _inventorySlots[i].enabled = true; // Включить отображение спрайта
                Debug.Log($"Updated slot {i}");
            }
            else
            {
                _inventorySlots[i].sprite = null;
                _inventorySlots[i].enabled = false; // Скрыть слот, если он пустой
                Debug.Log($"Cleared slot {i}");
            }
        }
    }
    
    private void HandleSelectorMovement()
    {
        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                _selector.transform.position = new Vector3(
                    _inventorySlots[i].transform.position.x,
                    _selector.transform.position.y,
                    _selector.transform.position.z
                );
                Debug.Log($"Selector moved to slot {i}");
                break;
            }
        }
    }
}
