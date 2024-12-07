using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public InventoryManager Inventory;
	private PlayerStatus _playerStatus;

	void Start()
	{
		Inventory = GetComponent<InventoryManager>();
		_playerStatus = GetComponent<PlayerStatus>();
	}

	private void OnCollisionStay(Collision other)
	{
		if (other.gameObject.TryGetComponent(out FireManager manager))
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				Item selectedItem = Inventory.GetSelectedItem();
				if (selectedItem != null)
				{
					switch (selectedItem.itemType)
					{
						case ItemType.Log:
							manager.AddHeat(selectedItem.burnTimeAmount);
							manager.AddHeat(selectedItem.heatIncreaseAmount);
							Inventory.RemoveSelectedItem();
							Debug.Log("Added burn time: " + selectedItem.burnTimeAmount + " and heat: " + selectedItem.heatIncreaseAmount);
							break;
						case ItemType.Stone:
							manager.AddStone();
							Inventory.RemoveSelectedItem();
							Debug.Log("Added stone to increase fire stage.");
							break;
						default:
							Debug.Log("Selected item cannot be used on fire.");
							break;
					}
				}
				else
				{
					Debug.Log("No item selected to add to fire.");
				}
			}
		}
	}
}
