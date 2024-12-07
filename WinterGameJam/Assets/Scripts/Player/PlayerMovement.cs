using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _baseSpeed = 3f;
	[SerializeField] private float _acceleration = 10f;
	[SerializeField] private float _deceleration = 10f;
	private Rigidbody _rb;
	private PlayerStatus _playerStatus;
	private Vector3 _currentVelocity;
	private bool _facingRight = true;

	private Quaternion _rightRotation = Quaternion.Euler(-90, 0, -180);
	private Quaternion _leftRotation = Quaternion.Euler(90, -180, -180);

	private float _speedMultiplier = 1f;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_playerStatus = GetComponent<PlayerStatus>();
	}

	private void FixedUpdate()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");
		Vector3 targetDirection = new Vector3(horizontal, 0, vertical).normalized;

		float currentSpeed = _baseSpeed * _playerStatus.GetSpeedMultiplier() * _speedMultiplier;
		Vector3 targetVelocity = targetDirection * currentSpeed;

		if (targetDirection != Vector3.zero)
		{
			_currentVelocity = Vector3.Lerp(_currentVelocity, targetVelocity, _acceleration * Time.fixedDeltaTime);
		}
		else
		{
			_currentVelocity = Vector3.Lerp(_currentVelocity, Vector3.zero, _deceleration * Time.fixedDeltaTime);
		}

		_rb.velocity = _currentVelocity;

		HandleRotation(horizontal);
	}

	private void HandleRotation(float horizontal)
	{
		if (horizontal > 0 && !_facingRight)
		{
			RotateCharacter(_rightRotation);
			_facingRight = true;
		}
		else if (horizontal < 0 && _facingRight)
		{
			RotateCharacter(_leftRotation);
			_facingRight = false;
		}
	}

	private void RotateCharacter(Quaternion rotation)
	{
		transform.rotation = rotation;
	}

	public void ApplySpeedMultiplier(float multiplier)
	{
		_speedMultiplier = multiplier;
	}
}
