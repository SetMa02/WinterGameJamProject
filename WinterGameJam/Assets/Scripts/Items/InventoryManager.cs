using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	[SerializeField] private ItemType[] _slots = new ItemType[4];
	private int _selectedSlot = 0;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1)) _selectedSlot = 0;
		if (Input.GetKeyDown(KeyCode.Alpha2)) _selectedSlot = 1;
		if (Input.GetKeyDown(KeyCode.Alpha3)) _selectedSlot = 2;
		if (Input.GetKeyDown(KeyCode.Alpha4)) _selectedSlot = 3;
	}

	public bool AddItem(ItemType item)
	{
		for (int i = 0; i < _slots.Length; i++)
		{
			if (_slots[i] == default(ItemType))
			{
				_slots[i] = item;
				return true;
			}
		}
		return false;
	}

	public bool RemoveItem(ItemType item)
	{
		for (int i = 0; i < _slots.Length; i++)
		{
			if (_slots[i] == item)
			{
				_slots[i] = default(ItemType);
				return true;
			}
		}
		return false;
	}

	public bool HasItem(ItemType item)
	{
		for (int i = 0; i < _slots.Length; i++)
		{
			if (_slots[i] == item)
				return true;
		}
		return false;
	}

	public ItemType GetSelectedItem()
	{
		return _slots[_selectedSlot];
	}

	public bool RemoveSelectedItem()
	{
		if (_slots[_selectedSlot] != default(ItemType))
		{
			_slots[_selectedSlot] = default(ItemType);
			return true;
		}
		return false;
	}
}
