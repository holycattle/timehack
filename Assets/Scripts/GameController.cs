using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private static float TEN = 10;
	private static GameController _singleton;

	// Timer
	protected float timeStart = -1;
	protected Material timerMaterial;

	void Awake() {
		_singleton = this;
		timerMaterial = GameObject.Find("_HUDCamera").GetComponentInChildren<MeshRenderer>().material;
	}

	void Start() {
		timeStart = Time.time;
	}

	void Update() {
		float timePassed = Time.time - timeStart;
		int currentNumber = (int)Mathf.Min(Mathf.FloorToInt((TEN + 1) - timePassed), TEN);
		timerMaterial.mainTextureOffset = new Vector2((TEN - currentNumber) / TEN, timerMaterial.mainTextureOffset.y);

		if (timePassed > TEN) {
			// Do Something!
			StartTimer();
		}
	}

	public void StartTimer() {
		timeStart = Time.time;
	}

	public static GameController GetInstance {
		get { return _singleton; }
	}
}
