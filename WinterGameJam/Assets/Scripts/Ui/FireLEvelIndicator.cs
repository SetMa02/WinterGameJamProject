using System;
using UnityEngine;
using UnityEngine.UI;

public class FireLevelIndicator : MonoBehaviour
{
	[SerializeField] private FireManager _fireManager;
	public Slider _slider;

	private void Start()
	{
		_slider = GetComponentInChildren<Slider>();

		if (_fireManager != null)
		{
			_slider.maxValue = _fireManager.maxHeat;
		}
		else
		{
			Debug.LogError("FireManager is not assigned in FireLevelIndicator.");
		}
	}

	private void Update()
	{
		if (_fireManager != null)
		{
			_slider.value = _fireManager.currentHeat;
		}
	}
}
