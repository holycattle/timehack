using UnityEngine;
using System.Collections;

public class FollowScript : MonoBehaviour {

	public Transform follow;
	private Vector3 offset;

	void Start() {
		offset = offset - follow.position;
	}

	void Update() {
		transform.position = follow.position - offset;
	}
}
