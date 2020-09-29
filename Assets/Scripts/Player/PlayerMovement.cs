using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public float speed = 6.0f;

	private int floorMask;
	private float camRayLength = 100.0f;

	private Vector3 movement;
	private Animator playerAnimator;
	private Rigidbody playerRigidbody;

	void Awake() {

		// Create layer mask for the floor layer
		floorMask = LayerMask.GetMask("Floor");

		// Set up the references
		playerAnimator = GetComponent<Animator>();
		playerRigidbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {

		// Get the input axes
		float horizontal = Input.GetAxisRaw("Horizontal");
		float vertical = Input.GetAxisRaw("Vertical");

		// Move the player
		Move(horizontal, vertical);

		// Turn the player
		Turning();

		// Animate the player
		Animating(horizontal, vertical);
	}

	private void Move(float horizontal, float vertical) {

		// Set the movement based on the input
		movement.Set(horizontal, 0.0f, vertical);

		// Normalise the movement vector and make it proportional to the speed per second
		movement = movement.normalized * speed * Time.deltaTime;

		// Move the player from its current position
		playerRigidbody.MovePosition(transform.position + movement);
	}

	private void Turning() {

		// Create a ray from the mouse cursor in the direction of camera
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Cast ray in order to check if it hits the floor
		RaycastHit floorHit;
		if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask)) {

			// Create a vector from the player to the points on the floor that ray hit
			Vector3 playerToMouse = floorHit.point - transform.position;

			// Restrict the vector to floor plane
			playerToMouse.y = 0f;

			// Set the player's rotation
			Quaternion rotation = Quaternion.LookRotation(playerToMouse);
			playerRigidbody.MoveRotation(rotation);
		}
	}

	private void Animating(float horizontal, float vertical) {

		// Check if there is any input and tell the animator
		bool walking = horizontal != 0 || vertical != 0;
		playerAnimator.SetBool("IsWalking", walking);
	}
}
