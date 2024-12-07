using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator), typeof(FireAnimationManager))]
public class FireManager : MonoBehaviour
{
	[SerializeField] private List<FireStage> _fireStages = new List<FireStage>();
	private FireStage _currentStage;

	public float maxHeat;
	public float currentHeat;
	public float HeatPerSecond;
	public float FireSize;
	private int _currentStageIndex;

	private void Start()
	{
		_currentStage = _fireStages[0];

		maxHeat = _currentStage.MaxFireHeat;
		currentHeat = maxHeat;

		_currentStageIndex = _fireStages.IndexOf(_currentStage);

		HeatPerSecond = _currentStage.HeatPerSecond;
		FireSize = _currentStage.FireSize;
	}

	private void Update()
	{

		currentHeat -= Time.deltaTime;
		if (currentHeat <= 0)
		{
			currentHeat = 0;
		}
	}

	public void GetCurrentStage()
	{
		_currentStage = _fireStages[_currentStageIndex];
	}

	public void AddHeat(float amount)
	{
		currentHeat += amount;
		currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);
	}

	public void AddStone()
	{
		if (_currentStageIndex < _fireStages.Count - 1)
		{
			_currentStageIndex++;
			_currentStage = _fireStages[_currentStageIndex];

			if (_currentStage == null) return;

			maxHeat = _currentStage.MaxFireHeat;
			HeatPerSecond = _currentStage.HeatPerSecond;
			FireSize = _currentStage.FireSize;

			transform.localScale = new Vector3(
				transform.localScale.x * _currentStage.FireSize,
				transform.localScale.y * _currentStage.FireSize,
				transform.localScale.z * _currentStage.FireSize
			);

			Debug.Log($"Fire level changed. Heat per second: {_currentStage.HeatPerSecond}");
		}
	}

	public void SphereEnter(PlayerStatus playerStatus)
	{
		playerStatus.SetFireProximityStatus(true, HeatPerSecond);
		Debug.Log($"Игрок вошел в зону костра {HeatPerSecond}");
	}

	public void SphereExit(PlayerStatus playerStatus)
	{
		playerStatus.SetFireProximityStatus(false, 0);
		Debug.Log("Игрок вышел из зоны костра");
	}
}