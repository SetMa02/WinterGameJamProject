using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStatus : MonoBehaviour
{
	[SerializeField] private float _maxTemperatire;
	[FormerlySerializedAs("_temperature")] [SerializeField] private float _currentTemperature;
	[SerializeField] private float _health = 100f;
	[SerializeField] private float _temperatureDecreaseRate = 1f;
	[SerializeField] private float _defaultTemperatureIncreaseRate = 0.5f;
	[SerializeField]private FireHeatTransfer _fireHeatTransfer;
	private float _coldEffectMultiplier = 1f;
	private float _gainingHeat = 0;

	private bool IsFireNear = false;
	
	private FireManager _currentFireManager;


	private void Start()
	{
		_maxTemperatire = 100f;
		_currentTemperature = _maxTemperatire;
	}

	void Update()
	{
		_currentTemperature -= Time.deltaTime;
	}

	public float GetPlayersTemperature()
	{
		return _currentTemperature;
	}

	public void HeatUp(float _heat, bool fireNear)
	{
		_currentTemperature = Mathf.Clamp(_currentTemperature +_heat, 0, 100);
	}
	
	
	public float GetSpeedMultiplier()
	{
		if (_currentTemperature < 50f)
			return 0.8f;
		return 1f;
	}

	public FireManager CurrentFireManager => _currentFireManager;

	private void Die()
	{
		Debug.Log("Игрок погиб!");
	}

	private void ApplyBuffs()
	{
		PlayerMovement playerMovement = GetComponent<PlayerMovement>();
		if (playerMovement != null)
		{
			playerMovement.ApplySpeedMultiplier(1.2f);
		}
	}

	private void RemoveBuffs()
	{
		PlayerMovement playerMovement = GetComponent<PlayerMovement>();
		if (playerMovement != null)
		{
			playerMovement.ApplySpeedMultiplier(1f);
		}
	}

	public void IncreaseColdEffect(float multiplier)
	{
		_coldEffectMultiplier = multiplier;
		Debug.Log("Cold effect multiplier set to: " + _coldEffectMultiplier);
	}
}
