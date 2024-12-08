using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ResourceItem : MonoBehaviour
{
	[SerializeField] private Item _item;
	public ItemType ItemType => _item != null ? _item.itemType : ItemType.Wood;
	private SpriteRenderer _spriteRenderer;

	private void Start()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();

		if (_item != null)
		{
			_spriteRenderer.sprite = _item.GetSprite();
			float randomYRotation = Random.Range(0f, 360f);
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, randomYRotation, transform.rotation.eulerAngles.z);
		}
		else
		{
			Debug.LogError("Item is not assigned in ResourceItem.");
		}
	}

	private void OnTriggerStay(Collider other)
	{
		PlayerInteraction player = other.GetComponent<PlayerInteraction>();
		if (player != null)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				if (player.Inventory.AddItem(_item))
				{
					SoundManager.Instance.PlayPickupSound(gameObject.transform.position);
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
}
