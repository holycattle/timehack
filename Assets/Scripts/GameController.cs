using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private static float TEN = 10;
	private static GameController _singleton;
	public bool timerPaused = false;

	// Timer
	protected float timerCountdown = -1;
	protected Material timerMaterial;

	// Score
	protected static int SCORE_PERSECOND = 10;
	protected float score = 0;
	protected int life = 0;

	void Awake() {
		_singleton = this;
		timerMaterial = GameObject.Find("_HUDCamera").transform.FindChild("SelfCountdownTimer").GetComponent<MeshRenderer>().material;
	}

	void Start() {
		timerCountdown = Time.time;
		score = 0;
	}

	void Update() {
		score += SCORE_PERSECOND * Time.deltaTime;

		if (!timerPaused) {
			timerCountdown -= Time.deltaTime;
			int currentNumber = Mathf.CeilToInt(timerCountdown);
			timerMaterial.mainTextureOffset = new Vector2((TEN - currentNumber) / TEN, timerMaterial.mainTextureOffset.y);

			if (timerCountdown <= 0) {
				// You Lose!
				StartTimer();
			}
		}
	}

	void OnGUI() {
		GUI.Box(new Rect(0, 0, 128, 32), "Score: " + (int)(score * 100) / 100);
		GUI.Box(new Rect(0, 32, 128, 32), "Hits: " + life);
	}

	public void WasHit() {
		life++;
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
