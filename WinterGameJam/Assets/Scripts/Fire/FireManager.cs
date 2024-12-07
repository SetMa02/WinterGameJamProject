using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Animator), typeof(FireHeatBuff))]
[RequireComponent(typeof(FireLevel), typeof(FireAnimationManager))]
public class FireManager : MonoBehaviour
{
	public float maxHeat;
	public float currentHeat;

	[SerializeField] private List<FireStage> _fireStages = new List<FireStage>();
	private FireHeatBuff _fireHeatBuff;
	private FireLevel _fireLevel;
	private FireAnimationManager _fireAnimationManager;
	private BoxCollider2D _boxCollider;
	private Animator _animator;
	private FireStage _currentStage;

	private void Start()
	{
		// Получение компонентов за один раз
		_fireHeatBuff = GetComponent<FireHeatBuff>();
		_fireLevel = GetComponent<FireLevel>();
		_fireAnimationManager = GetComponent<FireAnimationManager>();
		_animator = GetComponent<Animator>();
		_boxCollider = GetComponent<BoxCollider2D>();

		if (_fireStages.Count > 0)
		{
			_currentStage = _fireStages[0];
			_fireLevel.FireLevelUp(_currentStage);
		}
		else
		{
			Debug.LogError("No fire stages assigned in FireManager.");
		}
	}

	private void Update()
	{
		// Потеря тепла с учетом времени
		HeatLossPerSecond();
	}

	private void HeatLossPerSecond()
	{
		// Учет времени с deltaTime для более точного расчета
		currentHeat -= _fireLevel.HeatPerSecond * Time.deltaTime;

		// Применение ограничения на минимальный и максимальный уровень тепла
		currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);
	}

	public void AddFuel(Item item)
	{
		// Добавление топлива с учетом ограничения
		currentHeat = Mathf.Clamp(currentHeat + item.heatAmount, 0f, maxHeat);
	}

	public void AddStone()
	{
		// Логика для добавления камня, если требуется
	}
}
