using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Item")]
public class Item : ScriptableObject
{
	public Sprite icon;
	public float burnTimeAmount;      // Для Log: увеличение burnTime
	public float heatIncreaseAmount;  // Для Log: увеличение currentHeat
	public bool IsFuel;
	public ItemType itemType;         // Тип предмета
}
