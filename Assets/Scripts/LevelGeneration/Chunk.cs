using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour {

	public const int chunkSize = 32;
	public const float minBoxSize = 0.5f;
	public const float maxBoxSize = 4f;
	public const int minBoxCount = 4;
	public const int maxBoxCount = 10;
	public GameObject boxPrefab;

	void Start() {
		// Box Generation
		Vector3 src = transform.position;

		for (int i = 0; i < Random.Range(4, 10); i++) {
			GameObject g = Instantiate(boxPrefab,
				new Vector3(src.x + Random.Range(0, chunkSize), 0, src.z + Random.Range(0, chunkSize)),
				Quaternion.identity) as GameObject;
			Vector3 scale = new Vector3(Random.Range(minBoxSize, maxBoxSize),
				Random.Range(minBoxSize, maxBoxSize),
				Random.Range(minBoxSize, maxBoxSize));
			g.transform.localScale = scale;
			g.transform.position = new Vector3(g.transform.position.x, scale.y / 2f, g.transform.position.z);
			g.transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
		}
	}
}
