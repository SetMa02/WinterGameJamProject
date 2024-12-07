using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ResourceItem : MonoBehaviour
{
	[SerializeField] private Item _item;
	public ItemType ItemType => _item != null ? _item.itemType : ItemType.Branch;
	private SpriteRenderer _spriteRenderer;

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();

		if (_item != null)
		{
			_spriteRenderer.sprite = _item.icon;
		}
		else
		{
			Debug.LogError("Item is not assigned in ResourceItem.");
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		PlayerInteraction player = other.GetComponent<PlayerInteraction>();
		if (player != null)
		{
			if (player.Inventory.AddItem(_item))
			{
				Destroy(this.gameObject);
				Debug.Log("Item added to inventory: " + _item.name);
			}
			else
			{
				Debug.Log("Inventory is full. Cannot add item: " + _item.name);
			}
		}
	}
}
