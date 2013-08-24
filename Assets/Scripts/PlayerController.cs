using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 6;
	public FistController fist;

	//
	public Rigidbody playerBody;
	public Rigidbody fistBody;

	//
	protected bool isForward = true;

	void Awake() {
		fist = GetComponentInChildren<FistController>();
//		playerBody = transform.FindChild("Mesh").rigidbody;
//		fistBody = transform.FindChild("Fist").rigidbody;
//
//		if (playerBody == null)
//			Debug.LogWarning("NULL");
//		if (fist.GetCollider() == null)
//			Debug.LogWarning("FNULL");
//		Physics.IgnoreCollision(playerBody.gameObject.collider, fist.GetCollider());
	}

	void Start() {
	}

	void Update() {
		// Movement
		float xAxis = Input.GetAxis("Horizontal");
		float zAxis = Input.GetAxis("Vertical");
		transform.Translate(new Vector3(xAxis * moveSpeed * Time.deltaTime, 0, zAxis * moveSpeed * Time.deltaTime), Space.World);

		Vector3 rot = transform.rotation.eulerAngles;
		if (xAxis > 0) {
			rot.y = 0;
			isForward = true;
		} else if (xAxis < 0) {
			rot.y = 180;
			isForward = false;
		}

		transform.rotation = Quaternion.Euler(rot);

		// Attack
		if (Input.GetKeyDown(KeyCode.Mouse0)) {
			fist.Swing();
		}
	}
}
