using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(Animator), typeof(FireHeatBuff))]
[RequireComponent(typeof(FireLevel), typeof(FireAnimationManager))]
public class FireManager : MonoBehaviour
{
	public float maxHeat = 100f;
	public float currentHeat = 50f;

	[SerializeField] private List<FireStage> _fireStages = new List<FireStage>();
	private FireHeatBuff _fireHeatBuff;
	private FireLevel _fireLevel;
	private FireAnimationManager _fireAnimationManager;
	private BoxCollider2D _boxCollider;
	private Animator _animator;
	private FireStage _currentStage;
	private int _currentStageIndex = 0;

	[SerializeField] private float maxBurnTime = 300f; // Максимальное время горения (например, 5 минут)
	public float currentBurnTime { get; private set; }

	// References to visual components
	[SerializeField] private ParticleSystem fireParticleSystem;
	[SerializeField] private Light fireLight;

	private void Awake()
	{
		_fireHeatBuff = GetComponent<FireHeatBuff>();
		_fireLevel = GetComponent<FireLevel>();
		_fireAnimationManager = GetComponent<FireAnimationManager>();
		_animator = GetComponent<Animator>();
		_boxCollider = GetComponent<BoxCollider2D>();

		if (_fireStages == null || _fireStages.Count == 0)
		{
			Debug.LogError("No fire stages assigned in FireManager.");
			return;
		}

		for (int i = 0; i < _fireStages.Count; i++)
		{
			if (_fireStages[i] == null)
			{
				Debug.LogError($"FireStage at index {i} is null.");
				return;
			}
		}

		currentBurnTime = maxBurnTime;
	}

	private void Start()
	{
		_currentStage = _fireStages[_currentStageIndex];
		_fireLevel.FireLevelUp(_currentStage);
		Debug.Log($"FireManager initialized with stage: {_currentStage.name}");

		UpdateFireVisuals();
	}

	private void Update()
	{
		if (currentBurnTime > 0)
		{
			currentBurnTime -= Time.deltaTime;
			currentBurnTime = Mathf.Clamp(currentBurnTime, 0f, maxBurnTime);

			// Keep fire visuals active
			UpdateFireVisuals();
		}
		else
		{
			Debug.Log("Fire has extinguished.");
			// Disable fire visuals
			UpdateFireVisuals();
			// Дополнительная логика, например, отключение объекта или вызов события
		}

		HeatLossPerSecond();
	}

	private void HeatLossPerSecond()
	{
		if (currentBurnTime > 0)
		{
			currentHeat -= _fireLevel.HeatPerSecond * Time.deltaTime;
		}
		else
		{
			currentHeat = 0f;
		}

		currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);
	}

	public void AddBurnTime(float amount)
	{
		if (currentBurnTime <= 0)
		{
			Debug.Log("Cannot add burn time. Fire is already extinguished.");
			return;
		}

		currentBurnTime += amount;
		currentBurnTime = Mathf.Clamp(currentBurnTime, 0f, maxBurnTime);
		Debug.Log($"Burn time increased by {amount}. Current burn time: {currentBurnTime}");

		// Ensure fire visuals are active
		UpdateFireVisuals();
	}

	public void AddHeat(float amount)
	{
		currentHeat += amount;
		currentHeat = Mathf.Clamp(currentHeat, 0f, maxHeat);
		Debug.Log($"Heat increased by {amount}. Current heat: {currentHeat}");
		UpdateFireVisuals();
	}

	public void AddStone()
	{
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

	private void IncreaseFireStage()
	{
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
			Debug.Log("Fire stage automatically increased to: " + _currentStage.name);
		}
	}

	private void UpdateFireVisuals()
	{
		if (currentBurnTime > 0)
		{
			if (fireParticleSystem != null && !fireParticleSystem.isPlaying)
			{
				fireParticleSystem.Play();
			}

			if (fireLight != null)
			{
				fireLight.enabled = true;
				fireLight.intensity = _fireLevel.HeatPerSecond + (currentHeat / maxHeat * 5f); // Пример: интенсивность зависит от currentHeat
			}
		}
		else
		{
			if (fireParticleSystem != null && fireParticleSystem.isPlaying)
			{
				fireParticleSystem.Stop();
			}

			if (fireLight != null)
			{
				fireLight.enabled = false;
			}
		}
	}
}
