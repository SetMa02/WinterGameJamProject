using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float _baseSpeed = 3f;
	[SerializeField] private float _acceleration = 10f;
	[SerializeField] private float _deceleration = 10f;
	[SerializeField] private GameObject _leftFootstepPrefab;
	[SerializeField] private GameObject _rightFootstepPrefab;
	[SerializeField] private float _footstepSpawnDistance = 0.2f;
	[SerializeField] private float _footstepWidth = 0.18f;

	private Rigidbody _rb;
	private PlayerStatus _playerStatus;
	private Vector3 _currentVelocity;
	private bool _facingRight = true;

	private Quaternion _leftFootBaseRotation = Quaternion.Euler(90, 0, 0);
	private Quaternion _rightFootBaseRotation = Quaternion.Euler(90, 0, 0);
	private Quaternion _rightRotation = Quaternion.Euler(-90, 0, -180);
	private Quaternion _leftRotation = Quaternion.Euler(90, -180, -180);

	private float _speedMultiplier = 1f;
	private Vector3 _lastFootstepPosition;

	private GameObject[] _footstepSequence;
	private Quaternion[] _rotationSequence;
	private int _currentFootstepIndex = 0;

	private List<GameObject> _leftFootstepPool = new List<GameObject>();
	private List<GameObject> _rightFootstepPool = new List<GameObject>();
	private const int POOL_SIZE = 40;

	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
		_playerStatus = GetComponent<PlayerStatus>();

		_footstepSequence = new GameObject[]
		{
			_leftFootstepPrefab,
			_rightFootstepPrefab,
			_leftFootstepPrefab,
			_rightFootstepPrefab
		};

		_rotationSequence = new Quaternion[]
		{
			_leftFootBaseRotation,
			_rightFootBaseRotation,
			_leftFootBaseRotation,
			_rightFootBaseRotation
		};

		InitializeFootstepPools();
		_lastFootstepPosition = transform.position;
	}

	private void InitializeFootstepPools()
	{
		for (int i = 0; i < POOL_SIZE; i++)
		{
			GameObject leftFootstep = Instantiate(_leftFootstepPrefab, Vector3.zero, Quaternion.identity);
			leftFootstep.SetActive(false);
			_leftFootstepPool.Add(leftFootstep);

			GameObject rightFootstep = Instantiate(_rightFootstepPrefab, Vector3.zero, Quaternion.identity);
			rightFootstep.SetActive(false);
			_rightFootstepPool.Add(rightFootstep);
		}
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

			if (Vector3.Distance(transform.position, _lastFootstepPosition) >= _footstepSpawnDistance)
			{
				SpawnRealisticFootsteps(targetDirection);
				_lastFootstepPosition = transform.position;
			}
		}
		else
		{
			_currentVelocity = Vector3.Lerp(_currentVelocity, Vector3.zero, _deceleration * Time.fixedDeltaTime);
		}

		_rb.velocity = _currentVelocity;

		HandleRotation(horizontal);
	}

	private void SpawnRealisticFootsteps(Vector3 movementDirection)
	{
		Vector3 sideVector = Vector3.Cross(movementDirection, Vector3.up).normalized;

		GameObject currentFootstepPrefab = _footstepSequence[_currentFootstepIndex];
		Quaternion currentFootRotation = _rotationSequence[_currentFootstepIndex];

		SpawnSingleFootstep(
			currentFootstepPrefab,
			currentFootRotation,
			movementDirection,
			sideVector * (_currentFootstepIndex % 2 == 0 ? -_footstepWidth : _footstepWidth)
		);

		_currentFootstepIndex = (_currentFootstepIndex + 1) % _footstepSequence.Length;
	}

	private void SpawnSingleFootstep(GameObject footstepPrefab, Quaternion baseRotation, Vector3 movementDirection, Vector3 offsetVector)
	{
		GameObject footstep = GetPooledFootstep(footstepPrefab);

		if (footstep != null)
		{
			Vector3 footstepPosition = transform.position + offsetVector;

			footstep.transform.position = footstepPosition;

			Quaternion movementRotation = Quaternion.LookRotation(movementDirection);
			footstep.transform.rotation = movementRotation * baseRotation;

			footstep.SetActive(true);

			StartCoroutine(DeactivateFootstepAfterDelay(footstep));
		}
	}

	private GameObject GetPooledFootstep(GameObject prefab)
	{
		List<GameObject> pool = (prefab == _leftFootstepPrefab) ? _leftFootstepPool : _rightFootstepPool;

		for (int i = 0; i < pool.Count; i++)
		{
			if (!pool[i].activeInHierarchy)
			{
				return pool[i];
			}
		}

		GameObject newFootstep = Instantiate(prefab, Vector3.zero, Quaternion.identity);
		pool.Add(newFootstep);
		return newFootstep;
	}

	private System.Collections.IEnumerator DeactivateFootstepAfterDelay(GameObject footstep, float delay = 3f)
	{
		yield return new WaitForSeconds(delay);
		footstep.SetActive(false);
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