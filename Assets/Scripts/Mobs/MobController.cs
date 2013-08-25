using UnityEngine;
using System.Collections;

public class MobController : MonoBehaviour {

	public float moveSpeed;
	public float rotationSpeed = 180;
	public bool isAIEnabled = true;

	//
	private PlayerController player;

	// Explosion Data
	private float countdownTimer = 0;

	void Start() {
		moveSpeed = 1f;
		player = PlayerController.getPlayerControllerInstance();
	}

	void Update() {
		if (countdownTimer > 0) {
			countdownTimer -= Time.deltaTime;

			if (countdownTimer <= 0) {
				// Explode!
				Explode();
			}
		}

		if (isAIEnabled) {
			transform.LookAt(player.parentObject.transform.position);
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
		}
	}

	public void BlownAway() {
		GetComponent<PhysicsOffulator>().enabled = false;
		rigidbody.constraints = RigidbodyConstraints.None;
		rigidbody.useGravity = true;
		isAIEnabled = false;
	}

	public void SetFuse(float timeToExplode) {
		countdownTimer = timeToExplode;

		Debug.Log("Exploding in..." + timeToExplode);
	}

	public void Explode() {
		// Destroy This Object

		// Instantiate Explosion Stuff

	}
}
