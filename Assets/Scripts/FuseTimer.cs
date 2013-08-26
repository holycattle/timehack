using UnityEngine;
using System.Collections;

public class FuseTimer : MonoBehaviour {

	public bool isActive;
	public MobController monitor;

	//
	private Material mat;

	void Awake() {
		mat = GetComponent<MeshRenderer>().material;
		Debug.LogWarning("MatName: " + mat.name);
	}

	void Start() {
		IsActive = false;
	}

	void Update() {
		if (!isActive)
			return;
		if (monitor == null) {
			IsActive = false;
			return;
		}

		// Update Position
		Vector3 mobPosition = monitor.transform.position;
		Vector3 screenPos = PlayerController.getPlayerControllerInstance().camera.WorldToViewportPoint(mobPosition);
		transform.position = HUDCameraScript.Instance.cam.ViewportToWorldPoint(screenPos);

		// Update Texture
		float timeLeft = monitor.Timer;
		int currentNumber = (int)Mathf.Floor(timeLeft) + 1;
		renderer.material.mainTextureOffset = new Vector2((10 - currentNumber) / 10f, renderer.material.mainTextureOffset.y);

	}

	public bool IsActive {
		set {
			isActive = value;
			renderer.enabled = value;
		}
	}

	public MobController Monitor {
		set {
			monitor = value;
		}
	}
}
