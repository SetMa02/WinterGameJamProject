using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public InventoryManager inventory;
	public float interactRange = 2f;
	private FireManager fireManager;

	void Start()
	{
		inventory = GetComponent<InventoryManager>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			InteractWithObjects();
		}
	}

	void InteractWithObjects()
	{
	}

	void TryAddFuelToFire(FireManager fm)
	{
		if (inventory.HasItem(ItemType.Log))
		{
			//fm.AddFuel(ItemType.Log);
			inventory.RemoveItem(ItemType.Log);
		}
		else if (inventory.HasItem(ItemType.Board))
		{
			//fm.AddFuel(ItemType.Board);
			inventory.RemoveItem(ItemType.Board);
		}
		else if (inventory.HasItem(ItemType.Branch))
		{
			//fm.AddFuel(ItemType.Branch);
			inventory.RemoveItem(ItemType.Branch);
		}
		else if (inventory.HasItem(ItemType.Stone))
		{
			//fm.AddStone();
			inventory.RemoveItem(ItemType.Stone);
		}
	}
	// FIXME
}
