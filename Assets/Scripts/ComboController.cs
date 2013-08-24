using UnityEngine;
using System.Collections;

public class ComboController : MonoBehaviour {

	protected PlayerController player;
	public float swingSpeed = 2 * Mathf.PI; // Theta Per Second

	//
	private bool _isSwinging;
	private Quaternion _baseRotation;

	void Awake() {
		player = transform.parent.GetComponentInChildren<PlayerController>();
	}

	void Start() {
		_baseRotation = transform.rotation;
	}

	void Update() {
		if (!_isSwinging)
			return;

		Vector3 rot = transform.localRotation.eulerAngles;
		rot.y += swingSpeed * Time.deltaTime;

		if (rot.y > _baseRotation.eulerAngles.y + 180) {
			transform.localRotation = _baseRotation;
			_isSwinging = false;
		} else {
			transform.localRotation = Quaternion.Euler(rot);
		}
	}

	public void Swing() {
		_isSwinging = true;
	}

	public Collider GetCollider() {
		return GetComponentInChildren<Collider>();
	}
}
