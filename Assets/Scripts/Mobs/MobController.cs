using UnityEngine;
using System.Collections;

public class MobController : MonoBehaviour {
	public float moveSpeed;
	public float rotationSpeed = 180;
	private PlayerController player;
	// Use this for initialization
	void Start () {
		//TODO: initialize random stats here
		moveSpeed = 1f;
		player = PlayerController.getPlayerControllerInstance();
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(player.transform.position);
		transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime, Space.Self);
	}
}
