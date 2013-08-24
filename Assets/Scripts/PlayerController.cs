using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed = 6;

	void Awake() {
	}

	void Start() {
	}

	void Update() {
		float xAxis = Input.GetAxis("Horizontal");
		float zAxis = Input.GetAxis("Vertical");

		if (Mathf.Abs(xAxis) != 0) {
			Debug.Log("X Movement");
		}

		if (Mathf.Abs(zAxis) != 0) {
			Debug.Log("Y Movement");
		}

		transform.Translate(new Vector3(xAxis * moveSpeed * Time.deltaTime, 0, zAxis * moveSpeed * Time.deltaTime));
	}
}
