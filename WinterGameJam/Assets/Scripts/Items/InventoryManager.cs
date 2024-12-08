using System;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
	public event UnityAction OnInventoryChanged;
	public event UnityAction OnChopping;

	public Item[] _slots = new Item[4];
	private int _selectedSlot = 0;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1)) _selectedSlot = 0;
		if (Input.GetKeyDown(KeyCode.Alpha2)) _selectedSlot = 1;
		if (Input.GetKeyDown(KeyCode.Alpha3)) _selectedSlot = 2;
		if (Input.GetKeyDown(KeyCode.Alpha4)) _selectedSlot = 3;

		Debug.Log($"Selected slot: {_slots[_selectedSlot]}");
	}

	public bool AddItem(Item item)
	{
		for (int i = 0; i < _slots.Length; i++)
		{
			if (_slots[i] == null)
			{
				_slots[i] = item;
				OnInventoryChanged?.Invoke();
				return true;
			}
		}
		return false;
	}

	public bool RemoveItem(Item item)
	{
		for (int i = 0; i < _slots.Length; i++)
		{
			if (_slots[i] != null && _slots[i] == item)
			{
				_slots[i] = null;
				OnInventoryChanged?.Invoke();
				return true;
			}
		}
		return false;
	}

	public bool HasItem(Item item)
	{
		foreach (var invItem in _slots)
		{
			if (invItem == item)
				return true;
		}
		return false;
	}

	public bool HasByName(string itemName)
	{
		foreach (var item in _slots)
		{
			if (item != null && item.name.Equals(itemName, StringComparison.OrdinalIgnoreCase))
			{
				return true;
			}
		}
		return false;
	}

	public Item GetSelectedItem()
	{
		return _slots[_selectedSlot];
	}

	public void StartChopping()
	{
		OnChopping?.Invoke();
	}

	public bool RemoveSelectedItem()
	{
		if (_slots[_selectedSlot] != null)
		{
			_slots[_selectedSlot] = null;
			OnInventoryChanged?.Invoke();
			return true;
		}
		return false;
	}
}
