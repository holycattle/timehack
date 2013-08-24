using UnityEngine;
using System.Collections;

public class MobSpawnController : MonoBehaviour {
	//TODO: instantiate this programmitcally
	public GameObject mobPrefab;
	
	//number of mobs spawned per wave
	const int MOB_COUNT = 10;
	const float SPAWN_INTERVAL = 5f;
	const int RADIUS = 20;
	private float interval = 0f;
	private PlayerController player;
	
	public void spawnCircular(Vector3 center, float radius) {
		int angle = 0;
		Vector3 pointInCircle;
		for(int i=0; i < MOB_COUNT; i++) {
			angle = Random.Range(0, 360);
			pointInCircle = new Vector3(center.x + radius * Mathf.Sin(angle * Mathf.Deg2Rad), 0, center.z + radius * Mathf.Cos(angle * Mathf.Deg2Rad));
			GameObject g = Instantiate(mobPrefab, pointInCircle, Quaternion.identity) as GameObject;
			g.transform.parent = transform;
			Debug.Log(g.name);
		}
	}
	
	// Use this for initialization
	void Start () {
		player = PlayerController.getPlayerControllerInstance();
	}
	
	// Update is called once per frame
	void Update () {
		if(interval < SPAWN_INTERVAL) {
			interval += Time.deltaTime;
		} else {
			spawnCircular(player.transform.position, RADIUS);
			interval = 0;
		}
	}
}
