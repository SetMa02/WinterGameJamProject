using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
	public InventoryManager Inventory;
	[SerializeField] private float _interactionRange = 1.5f;

	private PlayerStatus _playerStatus;

	void Start()
	{
		Inventory = GetComponent<InventoryManager>();
		_playerStatus = GetComponent<PlayerStatus>();
		if (_playerStatus == null)
		{
			Debug.LogError("PlayerInteraction требует компонент PlayerStatus на том же объекте.");
		}
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
		if (_playerStatus.NearFire && _playerStatus.CurrentFireManager != null)
		{
			ItemType selectedItem = Inventory.GetSelectedItem();
			if (selectedItem != default(ItemType))
			{
				if (IsFuelItem(selectedItem))
				{
					_playerStatus.CurrentFireManager.AddFuel(selectedItem);
					Inventory.RemoveSelectedItem();
					Debug.Log($"Добавлено топливо: {selectedItem}");
				}
				else if (selectedItem == ItemType.Stone)
				{
					_playerStatus.CurrentFireManager.AddStone();
					Inventory.RemoveSelectedItem();
					Debug.Log("Добавлен камень.");
				}
			}
			else
			{
				Debug.Log("Нет выбранного предмета для передачи.");
			}
		}
		else
		{
			Debug.Log("Вы не рядом с костром.");
		}
	}

	bool IsFuelItem(ItemType item)
	{
		return (item == ItemType.Branch || item == ItemType.Board || item == ItemType.Log);
	}
}