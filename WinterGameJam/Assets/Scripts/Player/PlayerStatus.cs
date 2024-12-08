using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.VFX;

public class PlayerStatus : MonoBehaviour
{
	public VisualEffect vfxRenderer;
	[SerializeField] private float _zFogOffset;
	[SerializeField] private float _maxTemperature = 100f;
	[SerializeField] private float _currentTemperature;
	[SerializeField] private float _temperatureDecreaseRate = 5f;
	[SerializeField] private float _temperatureIncreaseRate = 5f;
	[SerializeField] private Image _uiFreezeEffect;
	private bool _isFrozen = false;

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
			UpdateTransparency();
		}

		_currentTemperature = Mathf.Clamp(_currentTemperature, 0, _maxTemperature);

		if (_currentTemperature <= 0)
		{
			Die();
		}
	}

	public void UpdateTransparency()
	{
		// Calculate transparency based on temperature ratio
		float temperatureRatio = Mathf.Clamp01(_currentTemperature / _maxTemperature);
        
		// Инвертируем соотношение, чтобы при высокой температуре было меньше прозрачности
		float transparency = 1f - temperatureRatio;
        
		// Получаем текущий цвет изображения
		Color currentColor = _uiFreezeEffect.color;
        
		// Устанавливаем новый цвет с измененной прозрачностью
		_uiFreezeEffect.color = new Color(
			currentColor.r, 
			currentColor.g, 
			currentColor.b, 
			transparency
		);
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
		if (!_isFrozen)
		{
			SoundManager.Instance.PlaySound("Замерзания", transform.position, 2f);
			Debug.Log("Игрок погиб!");
			_isFrozen = true;
		}
		
	}
}