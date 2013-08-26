using UnityEngine;
using System.Collections;

public class TimedDestroy : MonoBehaviour {

	public float destroyTime = 4f;

	void Update() {
		destroyTime -= Time.deltaTime;
		if (destroyTime <= 0)
			Destroy(gameObject);
	}
}
