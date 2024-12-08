using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class AnimationController : MonoBehaviour
{
	public bool IsPicking = false;
	public bool IsChopping = false;
	[SerializeField] private Animator _animator;
	[SerializeField] private float _animationDelay = 0.2f;
	[SerializeField] private float _fastForwardSpeed = 100f; // Ускорение старой анимации
	[SerializeField] private GameObject _sidePlayer;
	[SerializeField] private GameObject _frontPlayer;
	[SerializeField] private GameObject _backPlayer;
	[SerializeField] private Item _axe;

	private readonly string IsUpward = "IsUpward";
	private readonly string IsInFront = "IsInFront";
	private readonly string IsSideways = "IsSideways";
	private readonly string Speed = "Speed";
	private readonly string PickingTrigger = "IsPicking";
	private readonly string IsChoppingAnim = "IsChopping";
	private readonly string StartChoppingTrigger = "StartChopping";

	private float _chopTime = 3f;
	private Rigidbody _rigidbody;
	private bool _canSwitch = true;
	private string _currentAnimationState;
	private float _speed;
	private InventoryManager _inventoryManager;

	private void Start()
	{
		_inventoryManager = GetComponent<InventoryManager>();
		
		if (_animator == null)
		{
			_animator = GetComponent<Animator>();
		}

		_rigidbody = GetComponent<Rigidbody>();
		_currentAnimationState = IsInFront;

		_inventoryManager.OnInventoryChanged += PickItemAnimation;
		_inventoryManager.OnChopping += ChoppingAnimation;
	}

	private void OnDestroy()
	{
		_inventoryManager.OnInventoryChanged -= PickItemAnimation;
		_inventoryManager.OnChopping -= ChoppingAnimation;
	}

	private void Update()
	{
		_speed = _rigidbody.velocity.magnitude;
		_animator.SetFloat(Speed, _speed);

		if (_canSwitch)
		{
			TrackCurrentState();
		}
	}

	private void PlayChopSound()
	{
		SoundManager.Instance.PlaySound("УдарТопором", transform.position, 2f);
	}
	
	private void PickItemAnimation()
	{
		SwitchAnimation(PickingTrigger);
	}

	private void ChoppingAnimation()
	{
		if (_inventoryManager.HasItem(_axe))
		{
			IsChopping = true;
			_animator.SetTrigger(StartChoppingTrigger);
			_animator.SetBool(IsChoppingAnim,true);
			StartCoroutine(ChoppingDelay());
		}
	}

	private void TrackCurrentState()
	{
		if (Input.GetKeyDown(KeyCode.W))
		{
			SwitchAnimation(IsUpward);
		}
		else if (Input.GetKeyDown(KeyCode.S))
		{
			SwitchAnimation(IsInFront);
		}
		else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
		{
			SwitchAnimation(IsSideways);
		}
	}

	private void SwitchAnimation(string newState)
	{
		if (!_canSwitch) return;

		_canSwitch = false;

		if (_currentAnimationState != null)
		{
			StartCoroutine(FinishCurrentAnimation());
		}

		OffAllStates();
		_animator.SetBool(newState, true);
		_currentAnimationState = newState;

		StartCoroutine(EnableSwitchAfterDelay());
	}


	private IEnumerator FinishCurrentAnimation()
	{
		// Ускоряем текущую анимацию
		_animator.speed = _fastForwardSpeed;

		// Ждем одного кадра, чтобы анимация завершилась
		yield return null;

		// Возвращаем скорость анимации к нормальной
		_animator.speed = 1f;
	}

	private IEnumerator EnableSwitchAfterDelay()
	{
		yield return new WaitForSeconds(_animationDelay);
		_canSwitch = true;
	}

	private void OffAllStates()
	{
		_animator.SetBool(IsSideways, false);
		_animator.SetBool(IsInFront, false);
		_animator.SetBool(IsUpward, false);
	}

	private IEnumerator ChoppingDelay()
	{
		yield return new WaitForSeconds(_chopTime);
		_animator.SetBool(IsChoppingAnim, false);
		IsChopping = false;
		yield return null;
	}
}
