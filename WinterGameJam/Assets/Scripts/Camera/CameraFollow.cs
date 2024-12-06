using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;
	public float smoothSpeed = 0.127f;
	public Vector3 offset;

	private void LateUpdate()
	{
		if (target == null) return;

		transform.position = Vector3.Lerp(transform.position, target.position + offset, smoothSpeed);
	}
	// FIXME
}
