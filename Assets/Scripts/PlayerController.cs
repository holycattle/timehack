using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	//
	public float rotationSpeed = 180;
	public float moveSpeed = 6;

	//
	public Rigidbody playerBody;
	public Rigidbody fistBody;

	// Movement
	protected float yVel;
	protected bool isGrounded = true;
	protected int direction = 0;
	
	private static PlayerController playerControllerInstance;

	void Awake() {
		playerControllerInstance = this;
	}

	void Start() {
	}

	void FixedUpdate() {
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}
	
	public static PlayerController getPlayerControllerInstance() {
		return playerControllerInstance;
	}
	
	void Update() {
		// Movement
		float xAxis = Input.GetAxis("Horizontal");
		//z-axis is y-axis
		float zAxis = Input.GetAxis("Vertical");
		transform.Translate(Vector3.forward * zAxis * moveSpeed * Time.deltaTime, Space.Self);

		// Rotation
		Vector3 rot = transform.rotation.eulerAngles;
		rot.y += xAxis * rotationSpeed * Time.deltaTime;
		transform.rotation = Quaternion.Euler(rot);

		// Jump
		/*
		if (!isGrounded) {
			yVel += Constants.GRAVITY * Time.deltaTime;
			transform.Translate(new Vector3(0, yVel * Time.deltaTime, 0), Space.World);

			if (transform.position.y < 0) {
				Vector3 v = transform.position;
				v.y = 0;
				transform.position = v;
				isGrounded = true;
			}
		}
		if (Input.GetKeyDown(KeyCode.Space)) {
			if (isGrounded) {
				yVel = 5;
				isGrounded = false;
			}
		}
		*/
	}
}
