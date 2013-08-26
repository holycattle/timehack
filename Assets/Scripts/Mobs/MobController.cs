using UnityEngine;
using System.Collections;

public class MobController : MonoBehaviour {

	public GameObject bombPrefab;
	public float rotationSpeed = 180;
	public bool isAIEnabled = true;

	// Stats
	private float moveSpeed = 1f;

	// Color
	private Color currentColor;
	private Transform meshTransform;

	//
	private PlayerController player;

	// Explosion Data
	private float countdownTimer = 0;

	void Start() {
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
		FuseTimer t = HUDCameraScript.Instance.mobTimer;
		t.IsActive = true;
		t.Monitor = this;
		countdownTimer = timeToExplode;
	}

	public void ChangeColor() {
		meshTransform.renderer.material.color = Color.Lerp(currentColor, Color.blue, Mathf.PingPong(Time.time, Constants.POSSESSION_TIME) / Constants.POSSESSION_TIME);
	}

	public void Explode() {
		// Destroy This Object
		Destroy(gameObject);

		// Instantiate Explosion Stuff
		GameObject g = Instantiate(bombPrefab, transform.position, Quaternion.identity) as GameObject;
	}

	void OnCollisionEnter(Collision c) {
		if (!isAIEnabled) {
			if (c.gameObject.tag == "Mob") {
				if (player.parentObject == gameObject) {
					GameController.GetInstance.WasHit();
				}
			}
		}
	}

	public float Timer {
		get { return countdownTimer; }
	}

	public float MoveSpeed {
		set { moveSpeed = value; }
	}

}
