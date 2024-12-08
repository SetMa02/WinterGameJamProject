using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
	public List<Sprite> sprites;
	public float burnTimeAmount;      // Для Log: увеличение burnTime
	public float heatIncreaseAmount;  // Для Log: увеличение currentHeat
	public bool IsFuel;
	public ItemType itemType;         // Тип предмета
	private Sprite sprite = null;

	public Sprite GetSprite()
	{
		if (sprite == null)
		{
			sprite = sprites[Random.Range(0, sprites.Count)];
		}

		return sprite;
	}
}
