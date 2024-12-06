using System;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ResourceItem : MonoBehaviour
{
    [SerializeField] private Item _item;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (_item != null)
        {
            _spriteRenderer.sprite = _item.icon;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerInteraction player = other.GetComponent<PlayerInteraction>();
        if (player != null)
        {
            Debug.Log("Player entered");
            if (player.Inventory.AddItem(_item))
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
