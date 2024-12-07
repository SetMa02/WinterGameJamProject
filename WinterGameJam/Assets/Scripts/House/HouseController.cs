using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class HouseController : MonoBehaviour
{
	[SerializeField] private List<GameObject> _houseWalls = new List<GameObject>();

	private void OnTriggerStay(Collider other)
	{
		if (other.TryGetComponent<PlayerStatus>(out _))
		{
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
			foreach (var wall in _houseWalls)
			{
				wall.SetActive(true);
			}
		}
	}
}