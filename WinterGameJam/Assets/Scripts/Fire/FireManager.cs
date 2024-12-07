using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

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
	private FireHeatTransfer _fireHeatTransfer;
	private int _currentStageIndex;

	private void Start()
	{
		_fireHeatTransfer = GetComponent<FireHeatTransfer>();
		_fireLevel = GetComponent<FireLevel>();
		
		_currentStage = _fireStages[0];
		maxHeat = _currentStage.MaxFireHeat;
		currentHeat = maxHeat;
		_currentStageIndex = _fireStages.IndexOf(_currentStage);
		
		_fireHeatTransfer.SetHeatPerSecond(currentHeat);
		
		Debug.Log(currentHeat + " current heat transfer" + _fireHeatTransfer);
	}

	private void Update()
	{
		currentHeat -= Time.deltaTime;
	}

	public void AddHeat(float amount)
	{
		currentHeat += amount;
		currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);
	}
	
	
	public void AddStone()
	{
		_currentStageIndex = _fireStages.IndexOf(_currentStage);
		if (_currentStageIndex < _fireStages.Count - 1)
		{
			_currentStageIndex++;
			if (_fireStages[_currentStageIndex] == null)
			{
				Debug.LogError($"FireStage at index {_currentStageIndex} is null.");
				return;
			}
			_currentStage = _fireStages[_currentStageIndex];
			_fireLevel.FireLevelUp(_currentStage);
			Debug.Log("Fire stage increased to: " + _currentStage.name);
		}
		else
		{
			Debug.Log("Fire is already at maximum stage.");
		}
	}
	
	
}
