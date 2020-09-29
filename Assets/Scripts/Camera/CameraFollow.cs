using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothing = 5.0f;

	private Vector3 offset;

	void Start() {
		// Calculate the initial offset
		offset = transform.position - target.position;
	}

	void FixedUpdate() {

		// Create a position the camera is aiming for based on the offset from the target
		Vector3 targetCameraPosition = target.position + offset;

		// Smoothly interpolate between the camera's current position and it's target position
		transform.position = Vector3.Lerp(transform.position, targetCameraPosition, smoothing);
	}
}
