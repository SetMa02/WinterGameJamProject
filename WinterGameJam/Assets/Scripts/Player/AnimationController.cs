using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class AnimationController : MonoBehaviour
{
	[SerializeField] private Animator _animator;
	[SerializeField] private float _animationDelay = 0.2f;
	[SerializeField] private float _fastForwardSpeed = 100f; // Ускорение старой анимации
	[SerializeField] private GameObject _sidePlayer;
	[SerializeField] private GameObject _frontPlayer;
	[SerializeField] private GameObject _backPlayer;

	private readonly string IsUpward = "IsUpward";
	private readonly string IsInFront = "IsInFront";
	private readonly string IsSideways = "IsSideways";
	private readonly string Speed = "Speed";

	private Rigidbody _rigidbody;
	private bool _canSwitch = true;
	private string _currentAnimationState;
	private float _speed;

	private void Start()
	{
		if (_animator == null)
		{
			_animator = GetComponent<Animator>();
		}

		_rigidbody = GetComponent<Rigidbody>();
		_currentAnimationState = IsInFront;
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
}
