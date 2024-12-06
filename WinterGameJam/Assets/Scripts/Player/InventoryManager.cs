using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	public ItemType[] slots = new ItemType[4];

	public bool AddItem(ItemType item)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i] == default(ItemType))
			{
				slots[i] = item;
				return true;
			}
		}
		return false;
	}

	public bool RemoveItem(ItemType item)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i] == item)
			{
				slots[i] = default(ItemType);
				return true;
			}
		}
		return false;
	}

	public bool HasItem(ItemType item)
	{
		for (int i = 0; i < slots.Length; i++)
		{
			if (slots[i] == item)
				return true;
		}
		return false;
	}
}
