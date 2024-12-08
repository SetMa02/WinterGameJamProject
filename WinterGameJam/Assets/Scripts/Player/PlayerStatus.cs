using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.VFX;

public class PlayerStatus : MonoBehaviour
{
	public VisualEffect vfxRenderer;
	[SerializeField] private float _zFogOffset;
	[SerializeField] private float _maxTemperature = 100f;
	[SerializeField] private float _currentTemperature;
	[SerializeField] private float _temperatureDecreaseRate = 5f;
	[SerializeField] private float _temperatureIncreaseRate = 5f;

	private bool _isNearFire = false;
	private float _fireHeatAmount = 1f;
	private Vector3 _cleanFogSpot;

	private void Start()
	{
		_currentTemperature = _maxTemperature;
	}

	private void Update()
	{
		vfxRenderer.SetVector3("FFOOGG", new Vector3(transform.position.x + 5, transform.position.y,
			transform.position.z + _zFogOffset));
	}

	private void FixedUpdate()
	{
		if (_isNearFire)
		{
			_currentTemperature += _fireHeatAmount * _temperatureIncreaseRate;
			Debug.Log($"Нагрев у костра. {_fireHeatAmount}, Температура: {_currentTemperature}");
		}
		else
		{
			_currentTemperature -= _temperatureDecreaseRate;
			Debug.Log($"Охлаждение вне костра. Температура:");
		}

		_currentTemperature = Mathf.Clamp(_currentTemperature, 0, _maxTemperature);

		if (_currentTemperature <= 0)
		{
			Die();
		}
	}

	public void SetFireProximityStatus(bool isNearFire, float heatAmount)
	{
		_isNearFire = isNearFire;
		_fireHeatAmount = heatAmount;
		Debug.Log($"Статус у костра изменен. Огонь рядом: {_isNearFire}, Количество тепла: {_fireHeatAmount}");
	}

	public float GetPlayersTemperature()
	{
		return _currentTemperature;
	}

	public float GetSpeedMultiplier()
	{
		return _currentTemperature < 50f ? 0.8f : 1f;
	}

	private void Die()
	{
		Debug.Log("Игрок погиб!");
	}
}