using System;
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
			// Не работает ставит пустой спрайт
			//Sprite newSprite = _item.GetSprite();
			//if (newSprite != null)
			//{
				//_spriteRenderer.sprite = newSprite;
				float randomYRotation = UnityEngine.Random.Range(0f, 360f);
				transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, randomYRotation, transform.rotation.eulerAngles.z);
				//Debug.Log($"Спрайт установлен: {newSprite.name}");
			//}
			//else
			//{
				//Debug.LogError("GetSprite() вернул null. Проверьте список sprites в объекте Item.");
			//}
		}
		else
		{
			Debug.LogError("Item не назначен в ResourceItem.");
		}
	}

	private void OnTriggerStay(Collider other)
	{
		PlayerInteraction player = other.GetComponent<PlayerInteraction>();
		if (player != null)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				if (_item.name.Equals("Axe", StringComparison.OrdinalIgnoreCase))
				{
					if (player.Inventory.HasByName("Axe"))
					{
						Debug.Log("У вас уже есть топор.");
						return;
					}
				}

				if (player.Inventory.AddItem(_item))
				{
					SoundManager.Instance.PlayPickupSound(gameObject.transform.position);
					Destroy(this.gameObject);
					Debug.Log("Предмет добавлен в инвентарь: " + _item.name);
				}
				else
				{
					Debug.Log("Инвентарь полон. Невозможно добавить предмет: " + _item.name);
				}
			}
		}
	}
}
