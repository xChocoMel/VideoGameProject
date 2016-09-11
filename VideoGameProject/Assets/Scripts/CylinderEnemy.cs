using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CylinderEnemy : MonoBehaviour {

	public GameObject originalProjectile;

	void Start () {
		DontDestroyOnLoad (this.gameObject);
		StartCoroutine (Shoot ());
	}
	void Update () { 
	}

	IEnumerator Shoot() {
		while (true) {
			Instantiate(originalProjectile,transform.position,Quaternion.identity);
			yield return new WaitForSeconds (3);
		}
	}
}
