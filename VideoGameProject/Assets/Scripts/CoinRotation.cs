using UnityEngine;
using System.Collections;

public class CoinRotation : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
		if(speed==0)
		this.speed = 1;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (new Vector3 (60*speed, 0, 0) * Time.deltaTime); 
	}
}
