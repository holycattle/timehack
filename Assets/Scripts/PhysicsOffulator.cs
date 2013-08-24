using UnityEngine;
using System.Collections;

public class PhysicsOffulator : MonoBehaviour {
	void FixedUpdate() {
		if (rigidbody == null)
			return;
		rigidbody.velocity = Vector3.zero;
		rigidbody.angularVelocity = Vector3.zero;
	}
}
