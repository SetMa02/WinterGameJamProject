using System;
using UnityEngine;
using UnityEngine.Events;

public class InventoryManager : MonoBehaviour
{
	public event UnityAction OnInventoryChanged;
	public Item[] _slots = new Item[4];
	private int _selectedSlot = 0;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1)) _selectedSlot = 0;
		if (Input.GetKeyDown(KeyCode.Alpha2)) _selectedSlot = 1;
		if (Input.GetKeyDown(KeyCode.Alpha3)) _selectedSlot = 2;
		if (Input.GetKeyDown(KeyCode.Alpha4)) _selectedSlot = 3;
	}

	public bool AddItem(Item item)
	{
		for (int i = 0; i < _slots.Length; i++)
		{
			if (_slots[i] == default(Item))
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
			if (_slots[i] == item)
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
		for (int i = 0; i < _slots.Length; i++)
		{
			if (_slots[i] == item)
				return true;
		}
		return false;
	}

	public Item GetSelectedItem()
	{
		return _slots[_selectedSlot];
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
