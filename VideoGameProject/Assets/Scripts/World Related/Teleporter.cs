using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public float movX, movY, movZ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	
	}

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Player")){
			other.transform.position = new Vector3 (movX, movY, movZ);
		}
	}
}
