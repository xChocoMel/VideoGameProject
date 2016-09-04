using UnityEngine;
using System.Collections;

public class Node : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnDrawGizmos () {
		Gizmos.color = Color.gray;
		Gizmos.DrawSphere (transform.position, 1);
	}

	void OnDrawGizmosSelected () {
		Gizmos.color = Color.blue;
		Gizmos.DrawSphere (transform.position, 1);
	}

}
