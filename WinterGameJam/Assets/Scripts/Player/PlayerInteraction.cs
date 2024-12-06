using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public InventoryManager Inventory;
	[SerializeField] private float _interactionRange = 1.5f;
	[SerializeField] private FireManager _fireManager;

	void Start()
	{
		Inventory = GetComponent<InventoryManager>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			TryGiveItemToFire();
		}
	}

	void TryGiveItemToFire()
	{
		if (_fireManager == null) return;
		float distance = Vector2.Distance(this.transform.position, _fireManager.transform.position);
		if (distance <= _interactionRange)
		{
			ItemType selectedItem = Inventory.GetSelectedItem();
			if (selectedItem != default(ItemType))
			{
				if (IsFuelItem(selectedItem))
				{
					_fireManager.AddFuel(selectedItem);
					Inventory.RemoveSelectedItem();
				}
				else if (selectedItem == ItemType.Stone)
				{
					_fireManager.AddStone();
					Inventory.RemoveSelectedItem();
				}
			}
		}
	}

	bool IsFuelItem(ItemType item)
	{
		return (item == ItemType.Branch || item == ItemType.Board || item == ItemType.Log);
	}
}
