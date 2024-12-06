using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
	public float baseSpeed = 3f;
	private CharacterController characterController;
	private PlayerStatus playerStatus;

	void Start()
	{
		characterController = GetComponent<CharacterController>();
		playerStatus = GetComponent<PlayerStatus>();
	}

	void Update()
	{
		float horizontal = Input.GetAxis("Horizontal");
		float vertical = Input.GetAxis("Vertical");

		Vector3 direction = new Vector3(horizontal, 0, vertical);
		direction = Camera.main.transform.TransformDirection(direction);
		direction.y = 0f;

		float currentSpeed = baseSpeed * playerStatus.GetSpeedMultiplier();

		characterController.Move(direction * currentSpeed * Time.deltaTime);
	}
}
