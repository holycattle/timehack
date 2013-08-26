using UnityEngine;
using System.Collections;

public class HUDCameraScript : MonoBehaviour {

	private static HUDCameraScript _singleton;
	public Camera cam;
	private FuseTimer mobCountdownTimer;

	void Awake() {
		_singleton = this;
		cam = camera;
	}

	void Start() {
		mobCountdownTimer = GetComponentInChildren<FuseTimer>();
	}

	public static HUDCameraScript Instance {
		get { return _singleton; }
	}

	public FuseTimer mobTimer {
		get { return mobCountdownTimer; }
	}
}