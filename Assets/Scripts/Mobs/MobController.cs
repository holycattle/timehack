using UnityEngine;
using System.Collections;

public class MobController : MonoBehaviour {

	public float moveSpeed;
	public float rotationSpeed = 180;
	public bool isAIEnabled = true;
	
	// Color
	private Color currentColor;
	private Transform meshTransform;
	
	//
	private PlayerController player;

	// Explosion Data
	private float countdownTimer = 5f;

	void Start() {
		moveSpeed = 1f;
		player = PlayerController.getPlayerControllerInstance();
		meshTransform = transform.root.FindChild("Mesh");
		currentColor = meshTransform.renderer.material.color;
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
	
	public void ChangeColor() {
		meshTransform.renderer.material.color = Color.Lerp(currentColor, Color.blue, Mathf.PingPong (Time.time, Constants.POSSESSION_TIME) / Constants.POSSESSION_TIME);
	}
	
	public void Explode() {
		// Destroy This Object

		// Instantiate Explosion Stuff

	}
}
