using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	//
	public float rotationSpeed = 180;
	public float moveSpeed = 6;
	
	// Possession
	const float POSSESSION_TIME = 3f;
	private float possessionTime = 0;
	private bool isPossessing = false;
	private ControllableEntity targetEnemy;
	
	// Controllability
	public GameObject parentObject;
	public MobController mobController;

	// Movement
	protected float yVel;
	protected bool isGrounded = true;
	protected int direction = 0;
	private static PlayerController playerControllerInstance;
	
	//Color
	private Transform meshTransform;
	private Color currentColor;
	//how long it takes to possess a node

	void Awake() {
		playerControllerInstance = this;
		mobController = transform.root.GetComponentInChildren<MobController>();
		meshTransform = transform.root.FindChild("Mesh");
	}

	void Start() {
		parentObject = transform.root.gameObject;
		currentColor = meshTransform.renderer.material.color;
		mobController.isAIEnabled = false;
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
				isPossessing = true;
				targetEnemy = hit.transform.root.GetComponentInChildren<ControllableEntity>();
			}
		}
		
		if(isPossessing) Possess(targetEnemy);
	}
	
	private void Possess(ControllableEntity c) {
		meshTransform.renderer.material.color = Color.Lerp(currentColor, Color.blue, Mathf.PingPong (Time.time, Constants.POSSESSION_TIME) / Constants.POSSESSION_TIME);
		if (c != null && c != transform.root.GetComponent<ControllableEntity>()) {
			c.gameObject.GetComponentInChildren<MobController>().ChangeColor();
		}
		if(possessionTime < POSSESSION_TIME) {
			possessionTime += Time.deltaTime;
		} else {
			if (c != null && c != transform.root.GetComponent<ControllableEntity>())
				SetControllingObject(c.gameObject);
			possessionTime = 0;
			isPossessing = false;
			c = null;
		}
	}
	
	public void SetControllingObject(GameObject g) {
		// Set Fuse of Old Mob
		MobController mob = g.GetComponentInChildren<MobController>();

		// Swap Bodies
		Vector3 offset = transform.localPosition;
		Quaternion offrot = transform.localRotation;

		transform.parent = g.transform;
		transform.localPosition = offset;
		transform.localRotation = offrot;

		parentObject = g;

		mobController.isAIEnabled = true;
		mobController = transform.root.GetComponentInChildren<MobController>();
		mobController.isAIEnabled = false;

		// Resets Explosion Timer
		GameController.GetInstance.StartTimer();
	}
}
