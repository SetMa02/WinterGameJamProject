using UnityEngine;

public class CameraFollow : MonoBehaviour
{
		[SerializeField] private Transform _target;
		[SerializeField] private float _smoothSpeed = 0.125f;
		[SerializeField] private Vector3 _offset;
		[SerializeField] private float _zoomSpeed = 2f; // Скорость зума
		[SerializeField] private float _minZoom = 5f;  // Минимальное расстояние
		[SerializeField] private float _maxZoom = 15f; // Максимальное расстояние

		private void LateUpdate()
		{
			if (_target == null) return;

			// Обработка прокрутки колесика мыши для зума
			float scrollInput = Input.GetAxis("Mouse ScrollWheel");
			if (scrollInput != 0)
			{
				// Изменение смещения по оси Y (например, отдаление или приближение)
				_offset.y = Mathf.Clamp(_offset.y - scrollInput * _zoomSpeed, _minZoom, _maxZoom);
			}

			// Обновление позиции камеры с плавным движением
			Vector3 desiredPosition = _target.position + _offset;
			Vector3 smoothed = Vector3.Lerp(this.transform.position, desiredPosition, _smoothSpeed);
			this.transform.position = smoothed;
		}
}
