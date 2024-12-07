using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayersHeatDisplay : MonoBehaviour
{
	[SerializeField] private PlayerStatus _playerStatus;
	private Slider _freezeDisplay;

	private void Start()
	{
		_freezeDisplay = GetComponentInChildren<Slider>();

		if (_freezeDisplay != null && _playerStatus != null)
		{
			_freezeDisplay.maxValue = _playerStatus.GetPlayersTemperature();
			_freezeDisplay.value = _playerStatus.GetPlayersTemperature();
		}
		else
		{
			Debug.LogError("Slider or PlayerStatus not assigned in PlayersHeatDisplay.");
		}
	}

	private void Update()
	{
		if (_freezeDisplay != null && _playerStatus != null)
		{
			_freezeDisplay.value = _freezeDisplay.maxValue - _playerStatus.GetPlayersTemperature();
		}
	}
}
