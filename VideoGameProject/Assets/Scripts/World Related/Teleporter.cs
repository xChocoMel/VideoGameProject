using UnityEngine;
using System.Collections;

public class Teleporter : MonoBehaviour {

	public float movX, movY, movZ;

	void OnTriggerEnter(Collider other) {
		if(other.gameObject.CompareTag("Player")){
			other.transform.position = new Vector3 (movX, movY, movZ);
		}
	}
}
