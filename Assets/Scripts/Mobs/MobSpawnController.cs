using UnityEngine;
using System.Collections;

public class MobSpawnController : MonoBehaviour {
	public GameObject mobPrefab;

	//
	const float JIGGLE_AMOUNT = 0.2f;
	const float MOVESPEED = 2f;

	//number of mobs spawned per wave
	private int mobCount = 20;
	const float SPAWN_INTERVAL = 5f;
	const int RADIUS = 10;
	private float interval = 0f;
	private PlayerController player;

	public void spawnCircular(Vector3 center, float radius) {
		int angle = 0;
		Vector3 pointInCircle;

		for (int i=0; i < mobCount; i++) {
			angle = Random.Range(0, 360);

			pointInCircle = new Vector3(center.x + radius * Random.Range(1 - JIGGLE_AMOUNT, 1 + JIGGLE_AMOUNT) * Mathf.Sin(angle * Mathf.Deg2Rad), 0,
				center.z + radius * Random.Range(1 - JIGGLE_AMOUNT, 1 + JIGGLE_AMOUNT) * Mathf.Cos(angle * Mathf.Deg2Rad));

			GameObject g = Instantiate(mobPrefab, pointInCircle, Quaternion.identity) as GameObject;

			// Randomize MoveSpeed
			MobController m = g.GetComponent<MobController>();
			m.MoveSpeed = MOVESPEED * Random.Range(1 - JIGGLE_AMOUNT, 1 + JIGGLE_AMOUNT);

			// Randomize Scale
			Vector3 lScale = m.transform.localScale;
			lScale = new Vector3(lScale.x * Random.Range(1 - JIGGLE_AMOUNT, 1 + JIGGLE_AMOUNT),
				lScale.y * Random.Range(1 - JIGGLE_AMOUNT, 1 + JIGGLE_AMOUNT),
				lScale.z * Random.Range(1 - JIGGLE_AMOUNT, 1 + JIGGLE_AMOUNT));
			m.transform.localScale = lScale;
		}
	}

	// Use this for initialization
	void Start() {
		player = PlayerController.getPlayerControllerInstance();
	}
	
	// Update is called once per frame
	void Update() {
		if (interval < SPAWN_INTERVAL) {
			interval += Time.deltaTime;
		} else {
			spawnCircular(player.parentObject.transform.position, RADIUS);
			interval = 0;
		}
	}
}
