using UnityEngine;

public class ResourceItem : MonoBehaviour
{
	public ItemType itemType;

	private void OnTriggerEnter(Collider other)
	{
		PlayerInteraction player = other.GetComponent<PlayerInteraction>();
		if (player != null)
		{
			if (player.inventory.AddItem(itemType))
			{
				gameObject.SetActive(false);
			}
		}
	}
}
