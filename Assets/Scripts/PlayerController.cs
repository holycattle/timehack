using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	//
	public float rotationSpeed = 180;
	public float moveSpeed = 6;

	//
	public GameObject parentObject;

	// Movement
	protected float yVel;
	protected bool isGrounded = true;
	protected int direction = 0;

	void Awake() {
	}

	void Start() {
		parentObject = transform.root.gameObject;
	}

	void Update() {
		// Movement
		float xAxis = Input.GetAxis("Horizontal");
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

		GameController.GetInstance.StartTimer();
	}
}
