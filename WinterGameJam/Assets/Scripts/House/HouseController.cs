using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HouseController : MonoBehaviour
{
	[SerializeField] private List<GameObject> _houseWalls = new List<GameObject>();

	private void OnTriggerEnter(Collider other)
	{
		if (other.TryGetComponent<PlayerStatus>(out _))
		{
			SoundManager.Instance.PlaySound("ОткрытияДвери", gameObject.transform.position);
			foreach (var wall in _houseWalls)
			{
				wall.SetActive(false);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.TryGetComponent<PlayerStatus>(out _))
		{
			SoundManager.Instance.PlaySound("ЗакрытияДвери", gameObject.transform.position);
			foreach (var wall in _houseWalls)
			{
				wall.SetActive(true);
			}
		}
	}
}