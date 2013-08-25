using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	//
	public float rotationSpeed = 180;
	public float moveSpeed = 6;

	// Controllability
	public GameObject parentObject;
	public MobController mobController;

	// Movement
	protected float yVel;
	protected bool isGrounded = true;
	protected int direction = 0;
	private static PlayerController playerControllerInstance;

	void Awake() {
		playerControllerInstance = this;
		mobController = transform.root.GetComponentInChildren<MobController>();
	}

	void Start() {
		parentObject = transform.root.gameObject;
		mobController.isEnabled = false;
	}
	
	public static PlayerController getPlayerControllerInstance() {
		return playerControllerInstance;
	}
	
	void Update() {
		// Movement
		float xAxis = Input.GetAxis("Horizontal");
		//z-axis is y-axis
		float zAxis = Input.GetAxis("Vertical");
		parentObject.transform.Translate(Vector3.forward * zAxis * moveSpeed * Time.deltaTime, Space.Self);

		// Rotation
		Vector3 rot = parentObject.transform.rotation.eulerAngles;
		rot.y += xAxis * rotationSpeed * Time.deltaTime;
		parentObject.transform.rotation = Quaternion.Euler(rot);


		/*
		 * Controls
		 */
		// Possession
		if (Input.GetMouseButton(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit, Mathf.Infinity)) {
				ControllableEntity c = hit.transform.root.GetComponentInChildren<ControllableEntity>();
				if (c != null)
					SetControllingObject(c.gameObject);
			}
		}
	}

	public void SetControllingObject(GameObject g) {
		Vector3 offset = transform.localPosition;
		Quaternion offrot = transform.localRotation;

		transform.parent = g.transform;
		transform.localPosition = offset;
		transform.localRotation = offrot;

		parentObject = g;

		mobController.isEnabled = true;
		mobController = transform.root.GetComponentInChildren<MobController>();
		mobController.isEnabled = false;

		GameController.GetInstance.StartTimer();
	}
}
