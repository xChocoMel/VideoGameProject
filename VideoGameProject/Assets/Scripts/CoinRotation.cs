using UnityEngine;
using System.Collections;

public class CoinRotation : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (60, 0, 0) * Time.deltaTime); 
	}
}
