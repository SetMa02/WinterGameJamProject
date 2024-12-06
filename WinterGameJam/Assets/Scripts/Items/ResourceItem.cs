using UnityEngine;

public class ResourceItem : MonoBehaviour
{
    [SerializeField] private ItemType _itemType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInteraction player = other.GetComponent<PlayerInteraction>();
        if (player != null)
        {
            if (player.Inventory.AddItem(_itemType))
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
