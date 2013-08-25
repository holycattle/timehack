using UnityEngine;
using System.Collections;

public class BombScript : MonoBehaviour {

	private float explosionRadius = 4f;
	private float explosionLength = 4f;
	private Rigidbody[] cubes;

	void Start() {
		/*
		 * "Particle Effects"
		 */
		// Get All Cubes
		cubes = transform.GetComponentsInChildren<Rigidbody>();

		// Jiggle Positions
		for (int i = 0; i < cubes.Length; i++)
			cubes[i].transform.position += new Vector3(Random.Range(-0.25f, 0.25f), Random.Range(0, 0.25f), Random.Range(-0.25f, 0.25f));

		// Add Explosion Force
		for (int i = 0; i < cubes.Length; i++)
			cubes[i].AddExplosionForce(40f, transform.position, explosionRadius);

		/*
		 * Explosion Force
		 */
		GameObject[] mobs = GameObject.FindGameObjectsWithTag("Mob");
		for (int i = 0; i < mobs.Length; i++) {
			if (Vector3.Distance(transform.position, mobs[i].transform.position) <= explosionRadius) {
				MobController m = mobs[i].GetComponent<MobController>();
				m.BlownAway();
				mobs[i].rigidbody.AddExplosionForce(400f, transform.position, explosionRadius);

				Debug.DrawRay(mobs[i].transform.position, Vector3.up, Color.red, 5f);
			}
		}
	}

	void Update() {
		explosionLength -= Time.deltaTime;
		if (explosionLength <= 0)
			Destroy(gameObject);
	}
}
