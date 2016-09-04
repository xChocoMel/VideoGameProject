using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	private Vector3 offset;
	public GameObject player;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset;
	}
}
