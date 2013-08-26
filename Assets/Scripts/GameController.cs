using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private static float TEN = 10;
	private static GameController _singleton;
	public bool timerPaused = false;

	// Timer
	protected float timerCountdown = -1;
	protected Material timerMaterial;

	void Awake() {
		_singleton = this;
		timerMaterial = GameObject.Find("_HUDCamera").transform.FindChild("SelfCountdownTimer").GetComponent<MeshRenderer>().material;
	}

	void Start() {
		timerCountdown = Time.time;
	}

	void Update() {
		if (timerPaused)
			return;

		timerCountdown -= Time.deltaTime;
		int currentNumber = Mathf.CeilToInt(timerCountdown);
		timerMaterial.mainTextureOffset = new Vector2((TEN - currentNumber) / TEN, timerMaterial.mainTextureOffset.y);

		if (timerCountdown <= 0) {
			// You Lose!
			StartTimer();
		}
	}

	public void StartTimer() {
		timerPaused = false;
		timerCountdown = TEN;
	}

	public float TimeRemaining {
		get { return timerCountdown; }
	}

	public static GameController GetInstance {
		get { return _singleton; }
	}
}
