using UnityEngine;
using System.Collections;

public class MobController : MonoBehaviour {

	public float moveSpeed;
	public float rotationSpeed = 180;
	public bool isEnabled = true;
	private PlayerController player;

	void Start() {
		moveSpeed = 1f;
		player = PlayerController.getPlayerControllerInstance();
	}
	
	void Update() {
		if (!isEnabled)
			return;
		
		transform.LookAt(player.parentObject.transform.position);
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
	}
}
