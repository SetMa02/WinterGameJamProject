using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Animator), typeof(FireAnimationManager), typeof(SpriteRenderer))]
public class FireManager : MonoBehaviour
{
	[SerializeField] private List<FireStage> _fireStages = new List<FireStage>();
	[SerializeField] private FireLevelIndicator _levelIndicator;
	public FireStage CurrentStage;
	private SpriteRenderer _spriteRenderer;

	public float LostHeatPerSecond = 0.2f;

	public bool IsFireActive = false;

	public float maxHeat;
	public float currentHeat;
	public float HeatPerSecond;
	public bool IsPlayerNearFire = false;
	public float FireSize;
	private int _currentStageIndex;
	private string _fireLevel = "FireLevel";
	private Animator _animator;
	private readonly string _isDead = "Isdead";

	private bool _hasPlayedExtinguishSound = false; // Флаг для отслеживания воспроизведения звука

	private void Start()
	{
		_animator = GetComponent<Animator>();
		_spriteRenderer = GetComponent<SpriteRenderer>();

		CurrentStage = _fireStages[0];
		maxHeat = CurrentStage.MaxFireHeat;
		currentHeat = maxHeat;

		_currentStageIndex = _fireStages.IndexOf(CurrentStage);

		HeatPerSecond = CurrentStage.HeatPerSecond;
		FireSize = CurrentStage.FireSize;

		_animator.SetFloat(_fireLevel, _currentStageIndex);
		IsFireActive = true;
	}

	private void FixedUpdate()
	{
		if (IsFireActive)
		{
			currentHeat -= LostHeatPerSecond;
		}

		if (currentHeat <= 0)
		{
			IsPlayerNearFire = false;

			if (!_hasPlayedExtinguishSound)
			{
				currentHeat = 0;
				HeatPerSecond = 0;
				IsFireActive = false;
				_animator.SetBool(_isDead, true);
				SoundManager.Instance.PlaySound("ПониженияУровня", transform.position);
				_hasPlayedExtinguishSound = true;
			}
		}
	}

	public void AddHeat(float amount)
	{
		currentHeat += amount;
		currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);

		CurrentStage = _fireStages[_currentStageIndex];

		if (CurrentStage == null) return;

		IsPlayerNearFire = true;
		HeatPerSecond = CurrentStage.HeatPerSecond;
		IsFireActive = true;

		_animator.SetBool(_isDead, false);
		_hasPlayedExtinguishSound = false;
	}

	public void AddStone()
	{
		if (_currentStageIndex < _fireStages.Count - 1)
		{
			_currentStageIndex++;

			CurrentStage = _fireStages[_currentStageIndex];
			if (CurrentStage == null) return;

			IsPlayerNearFire = true;
			maxHeat = CurrentStage.MaxFireHeat;
			HeatPerSecond = CurrentStage.HeatPerSecond;
			FireSize = CurrentStage.FireSize;
			
			_levelIndicator._slider.maxValue = maxHeat;
			_levelIndicator._slider.value = currentHeat;

			transform.localScale = new Vector3(
				transform.localScale.x * CurrentStage.FireSize,
				transform.localScale.y * CurrentStage.FireSize,
				transform.localScale.z * CurrentStage.FireSize
			);

			_animator.SetFloat(_fireLevel, _currentStageIndex);
			Debug.Log($"Fire level changed. Heat per second: {CurrentStage.HeatPerSecond}");

			_hasPlayedExtinguishSound = false;
			IsFireActive = true;
		}
	}

	public void SphereStay(PlayerStatus playerStatus)
	{
		playerStatus.SetFireProximityStatus(IsPlayerNearFire, HeatPerSecond);
		Debug.Log($"Игрок находится в зоне костра. Тепло: {HeatPerSecond}");
	}

	public void SphereEnter(PlayerStatus playerStatus)
	{
		IsPlayerNearFire = true;
		playerStatus.SetFireProximityStatus(IsPlayerNearFire, HeatPerSecond);
		Debug.Log($"Игрок вошел в зону костра. Тепло: {HeatPerSecond}");
	}

	public void SphereExit(PlayerStatus playerStatus)
	{
		IsPlayerNearFire = false;
		playerStatus.SetFireProximityStatus(IsPlayerNearFire, HeatPerSecond);
		Debug.Log("Игрок вышел из зоны костра");
	}
}
