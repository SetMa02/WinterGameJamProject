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
        _freezeDisplay.maxValue = _playerStatus.GetPlayersTemperature();
        _freezeDisplay.value = _playerStatus.GetPlayersTemperature();
    }

    private void Update()
    {
        _freezeDisplay.value = _freezeDisplay.maxValue - _playerStatus.GetPlayersTemperature();
    }
}
