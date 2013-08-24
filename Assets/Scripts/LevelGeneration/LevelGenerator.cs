using UnityEngine;
using System.Collections;

public class LevelGenerator : MonoBehaviour {

	public GameObject chunkPrefab;

	void Start() {
		// Generate the Level
		int s = 1;
		for (int i = -s; i <= s; i++) {
			for (int  o= -s; o <= s; o++) {
				GameObject g = Instantiate(chunkPrefab, new Vector3(i * 32, 0, o * 32), Quaternion.identity) as GameObject;
				g.transform.parent = transform;
			}
		}
	}
}
