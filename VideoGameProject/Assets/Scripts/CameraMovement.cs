using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	private Vector3 offset;
	public GameObject thePlayer;

	// Use this for initialization
	void Start () {
		thePlayer = GameObject.Find ("Player");
		offset = transform.position - thePlayer.transform.position;
	}

	// Update is called once per frame
	void LateUpdate () {
		transform.position = thePlayer.transform.position + offset;
	}
}
